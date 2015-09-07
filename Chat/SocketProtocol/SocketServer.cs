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
        public ServerListener serverListener;
        private int port;
        private ChatForm chatForm;
        // делегат для ожидания соединения
        public SocketServer(int port, ChatForm chatForm)
        {
            this.chatForm = chatForm;
            this.port = port;
        }

        public void start()
        {
            Listener = new TcpListener(IPAddress.Any, port);
            Listener.Start(); // Запускаем его
            serverListener = new ServerListener(Listener, chatForm);
            Thread backgroundThread = new Thread(new ThreadStart(serverListener.listenNewUser));
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
