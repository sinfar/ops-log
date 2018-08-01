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
    }
}
