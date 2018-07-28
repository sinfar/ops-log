using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.model
{
    class ProjectGroup
    {
        private int id;
        private string name;
        private List<Project> projects = new List<Project>();

        public int Id {
            get { return id; }
            set { id = value; }
        }
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Project> Projects
        {
            get { return projects; }
            set { projects = value; }
        }
    }
}
