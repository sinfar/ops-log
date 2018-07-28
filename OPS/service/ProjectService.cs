using OPS.dao;
using OPS.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.service
{
    class ProjectService
    {
        private Projectservice projectDAO = new Projectservice();

        // 查询所有项目组
        public List<ProjectGroup> getProjectGroups()
        {
            // 所有组
            List<ProjectGroup> groups = projectDAO.FindAllProjectGroups();

            // 所有项目
            List<Project> projects = projectDAO.FindAllProjects();

            // 组关联项目
            Dictionary<int, ProjectGroup> groupMap = new Dictionary<int, ProjectGroup>();
            foreach (ProjectGroup g in groups)
            {
                groupMap.Add(g.Id, g);
            }
            foreach (Project prj in projects)
            {
                ProjectGroup group = null;
                groupMap.TryGetValue(prj.GroupId, out group);
                if (group != null) {
                    group.Projects.Add(prj);
                }
            }

            return groups;
        }
    }
}
