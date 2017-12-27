using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chat.Message;
using Chat.SocketProtocol;

namespace Chat
{
    public partial class ChatForm : Form
    {
        private Boolean isServer;
        private SocketServer socketServer;
        private UserChat userChat;

        public ChatForm(bool isServer = false, UserInfo userInfo=null)
        {
            InitializeComponent();
            this.isServer = isServer;
            if (isServer)
            {
                socketServer = new SocketServer(8080, this);
                socketServer.start();
                ChatRichTextForm.AppendText("Server start\n");
                userChat = new UserChat(new UserInfo("server"), this);
            }
            else
            {
                userChat = new UserChat(userInfo, this);
                userChat.sendMessageObject(new TcpMessage(TcpMessage.TypeMsg.Login, "", userInfo.getName()));
                //userChat.sendMessage(userInfo.getName());
                Thread backgroundThread = new Thread(new ThreadStart(userChat.listenNewMessage));
                backgroundThread.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String msg = MessageRichTextBox.Text;
            MessageRichTextBox.Clear();
            ChatRichTextForm.AppendText(userChat.getUserInfo().getName()+":"+msg+"\n");
            sendMsg(new TcpMessage(TcpMessage.TypeMsg.Msg, msg, userChat.getUserInfo().getName()));
        }

        private void sendMsg(TcpMessage msg)
        {
            if (isServer)
            {
                socketServer.serverListener.sendAnotherUsersMessage(msg, null);
            }
            else
            {
                userChat.sendMessageObject(msg);
            }
        }


        public void printMessageChat(String message)
        {
            ChatRichTextForm.Invoke((MethodInvoker)delegate
            {
                ChatRichTextForm.AppendText(message + "\n");
            });
        }

        public void printError(TcpMessage msg)
        {
            printMessageChat("Error login user. Msg:"+msg.getMsg());
        }

        public void addUserToList(String login)
        {
            listUsers.Invoke((MethodInvoker)delegate
            {
                listUsers.Items.Add(login);
            }); 
        }

        public void removeUserToListByName(String login)
        {
            printMessageChat("User with login '" + login + "' has exited!");
            listUsers.Invoke((MethodInvoker)delegate
            {
                listUsers.Items.RemoveAt(listUsers.FindString(login));
            }); 
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isServer)
            {

            }
            else
            {
                userChat.sendMessageObject(new TcpMessage(TcpMessage.TypeMsg.Logout, "", userChat.getUserInfo().getName()));
                Thread.Sleep(1000);
                userChat.getUserInfo().getTcpClient().Close();
            }
        }


    }
}
