using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.model
{
    class Project
    {
        private int id;
        private string name;
        private int serverId;
        private string deployPath;
        private string logPath;
        private int groupId;

        private Server server;


        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ServerId
        {
            get { return serverId; }
            set { serverId = value; }
        }

        public string DeployPath
        {
            get { return deployPath; }
            set { deployPath = value; }
        }

        public int GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }

        internal Server Server
        {
            get
            {
                return server;
            }

            set
            {
                server = value;
            }
        }
    }
}
