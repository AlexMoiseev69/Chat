using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Message
{
    [Serializable]
    public class TcpMessage
    {
        public TcpMessage(TypeMsg type, String msg, String login)
        {
            this.type = type;
            this.msg = msg;
            this.login = login;
        }

        public enum TypeMsg
        {
            Login,
            Logout,
            Msg
        };

        private TypeMsg type;
        private String msg;
        private String login;

        public String getMsg()
        {
            return msg;
        }

        public TypeMsg getType()
        {
            return type;
        }

        public String getLogin()
        {
            return login;
        }
    }
}
