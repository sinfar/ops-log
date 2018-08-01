using OPS.dao;
using OPS.model;
using OPS.protocol;
using OPS.service;
using OPS.util;
using OPS.vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OPS
{

    public partial class Form1 : Form
    {
        private ProjectService projectService = new ProjectService();

        public Form1()
        {
            InitializeComponent();
            InitProjectTree();


        }

        // 初始化项目树
        private void InitProjectTree()
        {
            // 显示项目组
            List<ProjectGroup> groups = projectService.getProjectGroups();
            foreach (ProjectGroup group in groups)
            {
                TreeNode node = projectTree.Nodes.Add(group.Name);
                node.Name = group.Id.ToString();
                foreach (Project p in group.Projects)
                {
                    TreeNode childNode = node.Nodes.Add(p.Name);
                    childNode.Name = p.Id.ToString();
                }
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = projectTree.SelectedNode;
            int level = node.Level;
            if (level == 1) {
                int projectId = int.Parse(node.Name);
                LoadProjectFile(projectId);
            }
            
        }

        // 加载项目目录文件
        private void LoadProjectFile(int projectId)
        {
            fileListView.Clear();
            this.fileListView.BeginUpdate();

            // 查询数据
            List<FtpFile> files = projectService.getProjectLogFiles(projectId);
            FileListVO fileList = new FileListVO(files);



            fileListView.View = View.Details;
            fileListView.FullRowSelect = true;

            fileListView.LargeImageList = fileList.largeIconList;
            fileListView.SmallImageList = fileList.smallIconList;

            this.fileListView.Columns.Add("文件名", 240, HorizontalAlignment.Left);
            this.fileListView.Columns.Add("大小", 120, HorizontalAlignment.Right); 
            this.fileListView.Columns.Add("修改时间", 120, HorizontalAlignment.Left); 

            foreach (FileInfoVO info in fileList.list)
            {
                FileInfo f = info.fileInfo;
                ListViewItem item = new ListViewItem();
                item.ImageIndex = fileList.list.IndexOf(info);
                item.Text = f.name;
                item.SubItems.Add(f.isDirectory ? null: FileUtils.GetString(f.size));
                item.SubItems.Add(f.lastWriteTime.ToString());

                this.fileListView.Items.Add(item);
            }

            this.fileListView.EndUpdate();

        }
    }
}
