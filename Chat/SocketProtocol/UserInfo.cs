using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat.SocketProtocol
{
    public class UserInfo
    {
        private String name;
        private TcpClient tcpClient;

        public UserInfo(string name,  TcpClient tcpClient=null)
        {
            this.name = name;
            this.tcpClient = tcpClient;
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
