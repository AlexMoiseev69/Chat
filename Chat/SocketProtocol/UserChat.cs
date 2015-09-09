using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Message;
using SocketProtocol.Controller;

namespace Chat.SocketProtocol
{
    class UserChat
    {
        private UserInfo userInfo;
        private ServerListener serverListener;
        private ChatForm chatForm;

        public UserChat(UserInfo userInfo, ChatForm chatForm, ServerListener serverListener=null)
        {
            this.userInfo = userInfo;
            this.chatForm = chatForm;
            this.serverListener = serverListener;
        }

        public void listenNewMessage()
        {
            while (true)
            {
                TcpMessage msg = listenMsgObject();
                switch (msg.getType())
                {
                    case TcpMessage.TypeMsg.Msg:
                        chatForm.printMessageChat(msg.getLogin()+":" + msg.getMsg());
                        if (serverListener != null)
                        {
                            serverListener.sendAnotherUsersMessage(msg, userInfo.getTcpClient());
                        }
                        break;
                    case TcpMessage.TypeMsg.Logout:
                        //TODO: implement this
                        break;
                    default:
                        chatForm.printError(msg);
                        break;
                }
                
            }
        }

        private TcpMessage listenMsgObject()
        {
            NetworkStream stream = userInfo.getTcpClient().GetStream();
            IFormatter formatter = new BinaryFormatter();

            TcpMessage obj = (TcpMessage)formatter.Deserialize(stream);
            return obj;
        }

        public UserInfo getUserInfo()
        {
            return userInfo;
        }

        public void sendMessageObject(TcpMessage msg)
        {
            NetworkStream serverStream = userInfo.getTcpClient().GetStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(serverStream, msg);  
        }
    }
}
