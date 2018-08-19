using OPS.dao;
using OPS.model;
using OPS.protocol;
using OPS.protocol.ftp;
using OPS.service;
using OPS.util;
using OPS.vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace OPS
{

    public partial class Form1 : Form
    {
        private ProjectService projectService = new ProjectService();
        private ServerService serverService = new ServerService();
        private IFtpClient ftpClient = null;

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
            // 创建连接
            Project project = projectService.getProject(projectId);
            Server server = project.Server;

            try
            {
                ftpClient = serverService.connSftp(server);

                // 加载文件
                LoadFiles(project.LogPath);
            }
            catch (Exception e)
            {
                MessageBox.Show("FTP连接异常：" + e.Message);
                return;
            }
        }

        // 加载文件目录
        private void LoadFiles(String filepath)
        {
            // 查询数据
            FileListVO fileList = new FileListVO(new List<FtpFile>());
            try
            {
                List<FtpFile> files = ftpClient.getFileList(filepath);
                fileList = new FileListVO(files);
            }
            catch (Exception e)
            {
                Console.Error.Write(e);
                MessageBox.Show("FTP连接异常：" + e.Message);
                return;
            }

            // 显示数据
            fileListView.Clear();
            this.fileListView.BeginUpdate();

            fileListView.View = View.Details;
            fileListView.FullRowSelect = true;

            fileListView.LargeImageList = fileList.largeIconList;
            fileListView.SmallImageList = fileList.smallIconList;

            this.fileListView.Columns.Add("文件名", 240, HorizontalAlignment.Left);
            this.fileListView.Columns.Add("大小", 120, HorizontalAlignment.Right);
            this.fileListView.Columns.Add("修改时间", 120, HorizontalAlignment.Left);

            foreach (FileInfoVO info in fileList.list)
            {
                model.FileInfo f = info.fileInfo;
                ListViewItem item = new ListViewItem();
                item.ImageIndex = fileList.list.IndexOf(info);
                item.Text = f.name;
                item.SubItems.Add(f.isDirectory ? null : FileUtils.GetString(f.size));
                item.SubItems.Add(f.lastWriteTime.ToString());
                item.Tag = f;

                this.fileListView.Items.Add(item);
            }

            this.fileListView.EndUpdate();
        }

        // 下载文件到本地
        public void DownloadFile(string fileName)
        {
            // 选择路径
            string name = Path.GetFileName(fileName);
            string saveFileName = FileUtils.SaveFilePathName(name, null, null);

            if (saveFileName != null)
            {
                try
                {
                    ftpClient.DownloadFile(fileName, saveFileName);
                }
                catch (Exception e)
                {
                    MessageBox.Show("FTP连接异常：" + e.Message);
                    return;
                }
            }
        }

        private void ItemDoubleClick(object sender, EventArgs e)
        {
            SelectedListViewItemCollection items = fileListView.SelectedItems;
            if (items.Count > 0)
            {
                ListViewItem item = items[0];
                model.FileInfo fileInfo = (model.FileInfo)item.Tag;
                if (fileInfo.isDirectory)
                    LoadFiles(fileInfo.fullname);
                else
                    DownloadFile(fileInfo.fullname);
            }
        }
    }
}
