using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.SocketProtocol;

namespace Chat
{
    public partial class ChatForm : Form
    {
        private Boolean isServer;
        private SocketServer socketServer;
        private UserInfo userInfo;
        private TcpClient tcpClient;

        public ChatForm(bool isServer = false, TcpClient tcpClient = null)
        {
            InitializeComponent();
            this.isServer = isServer;
            if (isServer)
            {
                userInfo = new UserInfo("server");
            }
            else
            {
                userInfo = new UserInfo("User");
            }
            this.tcpClient = tcpClient;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String msg = MessageRichTextBox.Text;
            MessageRichTextBox.Clear();
            ChatRichTextForm.Text = userInfo.getName()+":"+msg;
            sendAnotherUsers(msg);
        }

        private void sendAnotherUsers(string msg)
        {
            if (isServer)
            {
                foreach (TcpClient tcpClient in socketServer.clientList.mapUsers.Values)
                {
                    NetworkStream serverStream = tcpClient.GetStream();
                    byte[] outStream = Encoding.ASCII.GetBytes(msg);
                    serverStream.Write(outStream, 0, outStream.Length);
                    serverStream.Flush();
                }
            }
            else
            {
                NetworkStream serverStream = tcpClient.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(msg);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (isServer)
            {
                socketServer = new SocketServer(8080, ChatRichTextForm);
            }
        }

        private void ChatForm_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
