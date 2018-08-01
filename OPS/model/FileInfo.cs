using OPS.protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.model
{
    class FileInfo
    {
        public string name;
        public string fullname;
        public long size;
        public DateTime lastWriteTime;
        public bool isDirectory;

        public FileInfo(FtpFile file)
        {
            this.name = file.name;
            this.fullname = file.name;
            this.size = file.size;
            this.lastWriteTime = file.lastWriteTime;
            this.isDirectory = file.isDirectory;
        }
    }
}
