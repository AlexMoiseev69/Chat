using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.SocketProtocol
{
    [Serializable]
    public class SocketMessageChat
    {

        public SocketMessageChat(String login, String msg)
        {
            this.login = login;
            this.msg = msg;
        }
        private String login;
        private String msg;

        public String getLogin()
        {
            return login;
        }

        public String getMsg()
        {
            return msg;
        }
    }
}
