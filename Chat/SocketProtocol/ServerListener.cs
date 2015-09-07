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
                //String login = getMessage(tcpClient);
                SocketMessageChat msg = getMessageObj(tcpClient);
                UserChat userChat = new UserChat(new UserInfo(msg.getLogin(), tcpClient), chatForm, this);
                Thread backgroundThread = new Thread(new ThreadStart(userChat.listenNewMessage));
                backgroundThread.Start();

                updateUsersList(userChat);
                
            }
        }

        private SocketMessageChat getMessageObj(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            IFormatter formatter = new BinaryFormatter();

            SocketMessageChat obj = (SocketMessageChat)formatter.Deserialize(stream);
            return obj;
        }

        private void updateUsersList(UserChat userChat)
        {
            mapUsers.Add(userChat.getUserInfo().getName(), userChat.getUserInfo().getTcpClient());
            chatForm.printMessageChat("User was connected with login: " + userChat.getUserInfo().getName());
            chatForm.addUserToList(userChat.getUserInfo().getName());
        }

        private String getMessage(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            byte[] bytes = new Byte[256];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            String msg = String.Empty;
            for (int i = 0; i < bytesRead; i++)
                msg += Convert.ToChar(bytes[i]);
            return msg;
        }


        public void sendAnotherUsersMessage(string msg, TcpClient tcpClientSender)
        {
            foreach (TcpClient tcpClient in mapUsers.Values)
            {
                if (tcpClient.Equals(tcpClientSender))
                    continue;
                NetworkStream serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(msg);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }
    }
}
