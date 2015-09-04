using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.SocketProtocol;

namespace SocketProtocol.Controller
{
    class ClientListener
    {
        private TcpListener Listener;
        private System.Windows.Forms.RichTextBox richTextBox1;
        public Dictionary<String, TcpClient> mapUsers = new Dictionary<string, TcpClient>(); 

        public ClientListener(TcpListener Listener, System.Windows.Forms.RichTextBox richTextBox1)
        {
            this.Listener = Listener;
            this.richTextBox1 = richTextBox1;
        }

        public void listenNewUser()
        {
            while (true)
            {
                TcpClient tcpClient = Listener.AcceptTcpClient();
             
                NetworkStream stream = tcpClient.GetStream();
                byte[] bytes = new Byte[256];
                int bytesRead = stream.Read(bytes, 0, bytes.Length);
                String login=String.Empty;
                for (int i = 0; i < bytesRead; i++)
                    login+=Convert.ToChar(bytes[i]);

                mapUsers.Add(login, tcpClient);
                printMessage("User was connected with login: "+login);
                Client client = new Client(new UserInfo(login, tcpClient), this.richTextBox1, this);
                Thread backgroundThread = new Thread(new ThreadStart(client.listenNewMessage));
                backgroundThread.Start();
            }
        }

        private void printMessage(String message)
        {
            this.richTextBox1.Invoke((MethodInvoker)delegate
            {
                richTextBox1.AppendText(message+"\n");
            });
        }
    }
}
