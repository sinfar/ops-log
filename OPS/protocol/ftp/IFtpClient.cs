using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.protocol
{
    interface IFtpClient
    {

        // 创建连接
        void Connect();

        // 关闭连接
        void Disconect();

        // 是否已连接
        bool Connected();

        // 获取目录下的文件
        List<FtpFile> getFileList(string path);

        // 下载一个文件到本地目录
        void DownloadFile(string remotePath, string localPath);

    }
}
