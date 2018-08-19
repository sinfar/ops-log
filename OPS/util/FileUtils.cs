using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.util
{
    class FileUtils
    {
        public static string GetString(long b)
        {
            const int GB = 1024 * 1024 * 1024;
            const int MB = 1024 * 1024;
            const int KB = 1024;


            if (b / GB >= 1)
            {
                return Math.Round(b / (float)GB, 2) + "GB";
            }


            if (b / MB >= 1)
            {
                return Math.Round(b / (float)MB, 2) + "MB";
            }


            if (b / KB >= 1)
            {
                return Math.Round(b / (float)KB, 2) + "KB";
            }


            return b + "B";
        }

        /// <summary>
        /// 选择保存文件的名称以及路径  取消返回null;
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="filter"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string SaveFilePathName(string fileName, string filter, string title)
        {
            string path = null;
            System.Windows.Forms.SaveFileDialog fbd = new System.Windows.Forms.SaveFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                fbd.FileName = fileName;
            }
            if (!string.IsNullOrEmpty(filter))
            {
                fbd.Filter = filter;// "Excel|*.xls;*.xlsx;";
            }
            if (!string.IsNullOrEmpty(title))
            {
                fbd.Title = title;// "保存为";
            }
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.FileName;
            }
            return path;
        }
    }



}
