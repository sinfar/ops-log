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
    }
}
