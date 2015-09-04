using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SocketProtocol.Controller;

namespace Chat.SocketProtocol
{
    internal class SocketServer
    {
        private TcpListener Listener; // Объект, принимающий TCP-клиентов
        public ClientListener clientList;
        // делегат для ожидания соединения
        public SocketServer(int port, System.Windows.Forms.RichTextBox richTextBox1)
        {
            Listener = new TcpListener(IPAddress.Any, port);

            Listener.Start(); // Запускаем его
            richTextBox1.Text = "Server start\n";
            clientList = new ClientListener(Listener, richTextBox1);

            Thread backgroundThread = new Thread(new ThreadStart(clientList.listenNewUser));
            backgroundThread.Start();
        }

        ~SocketServer()
        {
            // Если "слушатель" был создан
            if (Listener != null)
            {
                // Остановим его
                Listener.Stop();
            }
        }
    }
}
