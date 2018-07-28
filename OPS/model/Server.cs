using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.model
{
    class Server
    {
        private int id;
        private string name;
        private string ip;
        private string osUser;
        private string osPassword;
        private int sftpPort;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Ip
        {
            get
            {
                return ip;
            }

            set
            {
                ip = value;
            }
        }

        public string OsUser
        {
            get
            {
                return osUser;
            }

            set
            {
                osUser = value;
            }
        }

        public string OsPassword
        {
            get
            {
                return osPassword;
            }

            set
            {
                osPassword = value;
            }
        }

        public int SftpPort
        {
            get
            {
                return sftpPort;
            }

            set
            {
                sftpPort = value;
            }
        }
    }
}
