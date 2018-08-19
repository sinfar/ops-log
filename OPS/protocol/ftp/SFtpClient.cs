using OPS.protocol.ftp;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.protocol
{
   
    public class SFtpClient : IFtpClient
    {
        private SftpClient sftp = null;
        private string ip;
        private int port;
        private string username;
        private string password;

        public SFtpClient(string host, int port, string username, string password) {
            this.ip = host;
            this.port = port;
            this.username = username;
            this.password = password;
            sftp = new SftpClient(host, port, username, password);
        }

        public void Connect()
        {
            if (sftp != null)
            {
                try
                {
                    sftp.Connect();
                }
                catch(Exception e)
                {
                    throw new FtpException(e.Message, e);
                }
                
            }
            
        }

        public void Disconect()
        {
            if (sftp != null && sftp.IsConnected)
            {
                sftp.Disconnect();
            }
        }

        public bool Connected()
        {
            if (sftp == null)
                return false;

            return sftp.IsConnected;
        }

        public List<FtpFile> getFileList(string path)
        {
            IEnumerable<SftpFile> files = sftp.ListDirectory(path);
            List<FtpFile> fileList = new List<FtpFile>();
            foreach(SftpFile f in files)
            {
                if (f.IsDirectory && f.Name.Equals(".")) continue;

                FtpFile ftpFile = new FtpFile();
                ftpFile.name = f.Name;
                ftpFile.fullname = f.FullName;
                ftpFile.isDirectory = f.IsDirectory;
                ftpFile.size = f.Length;
                ftpFile.lastWriteTime = f.LastWriteTime;

                fileList.Add(ftpFile);
            }
            return fileList;
        }

        public void DownloadFile(string remotePath, string localPath)
        {
            DownloadFile(remotePath, localPath, null);
        }

        public void DownloadFile(string remotePath, string localPath, AsyncCallback callback)
        {
            SftpFileStream inSt = null;
            FileStream outSt = null;
            try
            {
                SftpFile file = sftp.Get(remotePath);
                long fileSize = file.Length;

                inSt = sftp.OpenRead(remotePath);
                outSt = new FileStream(localPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                byte[] buf = new byte[8092];
                int len = 0;
                long count = 0;
                while ((len = inSt.Read(buf, 0, 8092)) > 0)
                {
                    outSt.Write(buf, 0, len);
                    count += len;

                    if (callback != null) // 下载进度回调通知
                    {
                        DownloadAsyncResult result = new DownloadAsyncResult(count, fileSize);
                        callback.Invoke(result);
                    }
                }

            }
            finally
            {
                if (inSt != null) inSt.Close();
                if (outSt != null) outSt.Close();
            }
        }
    }
}
