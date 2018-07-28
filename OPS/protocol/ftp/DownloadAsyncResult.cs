using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OPS.protocol
{
    public class DownloadAsyncResult : IAsyncResult
    {
        private long completedLength;
        private long fileLength;


        public DownloadAsyncResult(long completedLength, long fileLength) {
            this.completedLength = completedLength;
            this.fileLength = fileLength;
        }

        public object AsyncState
        {
            get
            {
                return completedLength;
            }
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsCompleted
        {
            get
            {
                return completedLength == fileLength;
            }
        }
    }
}
