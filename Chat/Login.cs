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

                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(loginText.Text);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                ChatForm chatForm = new ChatForm(false, clientSocket);
                chatForm.Show();
                this.Hide();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception se)
            {
                Console.WriteLine("Unexpected exception : {0}", se.ToString());
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
