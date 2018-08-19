using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.protocol.ftp
{
    // 文件传输协议异常
    class FtpException:ApplicationException
    {
       
        public string message;
        private Exception innerException;

        public FtpException()
        {

        }

        public FtpException(string msg) : base(msg)
        {
            this.message = msg;
        }

        public FtpException(string msg, Exception innerException) : base(msg)
        {
            this.innerException = innerException;
            this.message = msg;
        }
    }
}
