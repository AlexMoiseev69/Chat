using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                String msg = listenMsg();
                chatForm.printMessageChat(userInfo.getName()+":" + msg);
                if (serverListener != null)
                {
                    serverListener.sendAnotherUsersMessage(userInfo.getName() + ":" + msg, userInfo.getTcpClient());
                }
            }
        }

        private String listenMsg()
        {
            NetworkStream stream = userInfo.getTcpClient().GetStream();
            byte[] bytes = new Byte[256];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);
            String msg = String.Empty;
            for (int i = 0; i < bytesRead; i++)
                msg += Convert.ToChar(bytes[i]);
            return msg;
        }
        private SocketMessageChat listenMsgObject()
        {
            NetworkStream stream = userInfo.getTcpClient().GetStream();
            IFormatter formatter = new BinaryFormatter();

            SocketMessageChat obj = (SocketMessageChat)formatter.Deserialize(stream);
            return obj;
        }

        public UserInfo getUserInfo()
        {
            return userInfo;
        }

        public void sendMessage(string msg)
        {
            NetworkStream serverStream = userInfo.getTcpClient().GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes(msg);
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public void sendMessageObject(SocketMessageChat msg)
        {
            NetworkStream serverStream = userInfo.getTcpClient().GetStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(serverStream, msg);  
        }
    }
}
