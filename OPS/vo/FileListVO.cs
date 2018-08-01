using OPS.model;
using OPS.protocol;
using OPS.util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OPS.vo
{
    class FileListVO
    {
        public List<FileInfoVO> list = new List<FileInfoVO>();
        public ImageList largeIconList = new ImageList();
        public ImageList smallIconList = new ImageList();

        public FileListVO(List<FtpFile> files)
        {
            foreach (FtpFile file in files)
            {
                FileInfoVO info = new FileInfoVO(file);
                list.Add(info);

                largeIconList.ImageSize = new Size(32, 32);
                largeIconList.Images.Add(info.largeIcon.ToBitmap());

                smallIconList.ImageSize = new Size(16, 16);
                smallIconList.Images.Add(info.smallIcon.ToBitmap());
            }
        }
    }

    class FileInfoVO
    {
        public model.FileInfo fileInfo;
        public Icon largeIcon;
        public Icon smallIcon;

        public FileInfoVO(FtpFile file)
        {
            this.fileInfo = new model.FileInfo(file);
            this.largeIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(file.name), true);
            this.smallIcon = GetSystemIcon.GetIconByFileType(Path.GetExtension(file.name), false);

            Console.WriteLine(file.name + "------->" + largeIcon + "," + smallIcon);
        }
    }

}
