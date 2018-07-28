using MySql.Data.MySqlClient;
using OPS.model;
using OPS.util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.dao
{
    class Projectservice
    {
        // 查询所有项目组
        public List<ProjectGroup> FindAllProjectGroups() {
            string sql = "select id, name from op_project_group";

            MySqlDataReader reader = DBUtils.ExecuteReader(DBUtils.Conn, CommandType.Text, sql, null);
            List<ProjectGroup> list = new List<ProjectGroup>();
            while (reader.Read())
            {
                ProjectGroup grp = new ProjectGroup();
                grp.Id = (int)reader[0];
                grp.Name = (string)reader[1];

                list.Add(grp);
            }
            
            return list;
        }

        // 查询所有项目
        public List<Project> FindAllProjects()
        {
            string sql = "select id, name, server_id, deploy_path, group_id from op_project";

            MySqlDataReader reader = DBUtils.ExecuteReader(DBUtils.Conn, CommandType.Text, sql, null);
            List<Project> list = new List<Project>();
            while (reader.Read())
            {
                Project prj = new Project();
                prj.Id = (int)reader[0];
                prj.Name = (string)reader[1];
                prj.ServerId = (int)reader[2];
                prj.DeployPath = (string)reader[3];
                prj.GroupId = (int)reader[4];

                list.Add(prj);
            }

            return list;
        }

        // 根据项目id查询项目，返回的项目绑定了server
        public Project findProject(int projectId)
        {
            string sql = "select " +
                            "a.id, a.name, a.server_id, a.deploy_path, a.log_path, a.group_id " +
                            "b.name server_name, b.ip, os_user, os_password " +
                         "from op_project a " +
                            "left join op_server b on b.id = a.server_id" + 
                         "where a.id = :id";

            MySqlParameter param = new MySqlParameter("id", projectId);

            MySqlDataReader reader = DBUtils.ExecuteReader(DBUtils.Conn, CommandType.Text, sql, param);
            Project list = new Project();
            if (reader.Read())
            {
                Project prj = new Project();
                prj.Id = (int)reader[0];
                prj.Name = (string)reader[1];
                prj.ServerId = (int)reader[2];
                prj.DeployPath = (string)reader[3];
                prj.GroupId = (int)reader[4];

                Server server = new Server();
                server.Id = (int)reader[5];
                server.Name = (string)reader[6];
                server.Ip = (string)reader[7];
                server.OsUser = (string)reader[8];
                server.OsPassword = (string)reader[9];

                prj.Server = server;

                return prj;
            }

            return null;
        }
    }
}
