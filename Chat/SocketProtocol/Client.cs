using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketProtocol.Controller;

namespace Chat.SocketProtocol
{
    class Client
    {
        private UserInfo userInfo;
        private RichTextBox richTextBox;
        private ClientListener clientListener;

        public Client(UserInfo userInfo, RichTextBox richTextBox, ClientListener clientListener=null)
        {
            this.userInfo = userInfo;
            this.richTextBox = richTextBox;
            this.clientListener = clientListener;
        }

        public void listenNewMessage()
        {
            while (true)
            {
                NetworkStream stream = userInfo.getTcpClient().GetStream();
                byte[] bytes = new Byte[256];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                String msg = String.Empty;
                for (int i = 0; i < bytesRead; i++)
                    msg += Convert.ToChar(bytes[i]);
                printMessage(userInfo.getName()+":" + msg);
                sendAnotherMessage(userInfo.getName() + ":" + msg, userInfo.getTcpClient());
            }
        }

        private void sendAnotherMessage(string msg, TcpClient tcpClientSender)
        {
            if (clientListener != null)
            {
                foreach (TcpClient tcpClient in clientListener.mapUsers.Values)
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

        private void printMessage(String message)
        {
            this.richTextBox.Invoke((MethodInvoker)delegate
            {
                richTextBox.AppendText(message + "\n");
            });
        }
    }
}
