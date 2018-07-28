using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPS.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPS.utils.Tests
{
    [TestClass()]
    public class FTPHelperTests
    {
        [TestMethod()]
        public void GetFilesDetailList()
        {
            string[] lines = FTPHelper.GetFilesDetailList();
            foreach(string s in lines)
            {
                Console.WriteLine("======================>" + s);
            }

            Assert.IsTrue(lines.Length > 0);
        }
    }
}