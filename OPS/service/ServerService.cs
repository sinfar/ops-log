using OPS.model;
using OPS.protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.service
{
    class ServerService
    {
        // 连接SFTP
        public SFtpClient connSftp(Server server)
        {
            SFtpClient ftp = new SFtpClient(server.Ip, server.SftpPort, server.OsUser, server.OsPassword);
            ftp.Connect();
            return ftp;
        }
    }
}
