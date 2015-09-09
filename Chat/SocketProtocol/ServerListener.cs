using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat;
using Chat.Message;
using Chat.SocketProtocol;

namespace SocketProtocol.Controller
{
    class ServerListener
    {
        private TcpListener Listener;
        public Dictionary<String, TcpClient> mapUsers = new Dictionary<string, TcpClient>();
        private ChatForm chatForm;

        public ServerListener(TcpListener listener, ChatForm chatForm)
        {
            this.Listener = listener;
            this.chatForm = chatForm;
        }

        public void listenNewUser()
        {
            while (true)
            {
                TcpClient tcpClient = Listener.AcceptTcpClient();
                TcpMessage msg = getTcpMessage(tcpClient);
                if (msg.getType().Equals(TcpMessage.TypeMsg.Login))
                {
                    UserChat userChat = new UserChat(new UserInfo(msg.getLogin(), tcpClient), chatForm, this);
                    Thread backgroundThread = new Thread(new ThreadStart(userChat.listenNewMessage));
                    backgroundThread.Start();
                    updateUsersList(userChat);
                }
                else
                {
                    chatForm.printError(msg);
                }
            }
        }

        private TcpMessage getTcpMessage(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            IFormatter formatter = new BinaryFormatter();

            TcpMessage obj = (TcpMessage)formatter.Deserialize(stream);
            return obj;
        }

        private void sendTcpMessage(TcpClient tcpClient, TcpMessage msg)
        {
            NetworkStream serverStream = tcpClient.GetStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(serverStream, msg);  
        }
        private void updateUsersList(UserChat userChat)
        {
            mapUsers.Add(userChat.getUserInfo().getName(), userChat.getUserInfo().getTcpClient());
            chatForm.printMessageChat("User was connected with login: " + userChat.getUserInfo().getName());
            chatForm.addUserToList(userChat.getUserInfo().getName());
        }

        public void sendAnotherUsersMessage(TcpMessage msg, TcpClient tcpClientSender)
        {
            foreach (TcpClient tcpClient in mapUsers.Values)
            {
                if (tcpClient.Equals(tcpClientSender))
                    continue;
                sendTcpMessage(tcpClient, msg);
            }
        }
    }
}
