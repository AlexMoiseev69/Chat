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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChatForm chatForm = new ChatForm(true);
            chatForm.Show();
            chatForm.Focus();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient clientSocket = new TcpClient();
            try
            {
                clientSocket.Connect(address.Text, Int32.Parse(port.Text));
                UserInfo userInfo = new UserInfo(loginText.Text, clientSocket);
                ChatForm chatForm = new ChatForm(false, userInfo);
                chatForm.Show();
                this.Hide();
            }
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                MessageBox.Show("SocketException : {0}", se.Message);
            }
            catch (Exception se)
            {
                MessageBox.Show("Unexpected exception : {0}", se.ToString());
            }
        }
    }
}
