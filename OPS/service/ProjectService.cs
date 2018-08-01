using OPS.dao;
using OPS.model;
using OPS.protocol;
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
        private ServerService serverService = new ServerService();

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

        // 查询项目日志
        public List<FtpFile> getProjectLogFiles(int projectId)
        {
            Project prj = projectDAO.findProject(projectId);
            Server server = prj.Server;

            SFtpClient ftp = serverService.connSftp(server);
            List<FtpFile> logFiles = ftp.getFileList(prj.LogPath);

            return logFiles;
        }

       

    }
}
