using OPS.dao;
using OPS.model;
using OPS.service;
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
        private void LoadProjectFile(int projectId) {
            fileListView.View = View.Details;
            this.fileListView.Columns.Add("列标题1", 120, HorizontalAlignment.Left); //一步添加
            this.fileListView.Columns.Add("列标题2", 120, HorizontalAlignment.Left); //一步添加
            this.fileListView.Columns.Add("列标题3", 120, HorizontalAlignment.Left); //一步添加

            this.fileListView.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            for (int i = 0; i < 10; i++)   //添加10行数据
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = "subitem" + i;

                lvi.SubItems.Add("第2列,第" + i + "行");

                lvi.SubItems.Add("第3列,第" + i + "行");

                this.fileListView.Items.Add(lvi);
            }

            this.fileListView.EndUpdate();  //结束数据处理，UI界面一次性绘制。

        }
    }
}
