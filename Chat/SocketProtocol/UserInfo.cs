using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat.SocketProtocol
{
    class UserInfo
    {
        private String name;
        private TcpClient tcpClient;
        private string p;


        public UserInfo(string name,  TcpClient tcpClient)
        {
            this.name = name;
            this.tcpClient = tcpClient;
        }

        public UserInfo(string p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public String getName()
        {
            return name;
        }


        public void setName(String name)
        {
            this.name=name;
        }

        public TcpClient getTcpClient()
        {
            return tcpClient;
        }

    }
}
