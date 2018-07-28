using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OPS.utils;
using System.Collections;

namespace OPSTests.util
{
    [TestClass]
    public class SFTPHelperTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            SFTPHelper sftp = new SFTPHelper("192.168.164.131", "22", "root", "123456");
            ArrayList files = sftp.GetFileList("/data/logs");
            foreach (string s in files)
            {
             Console.WriteLine(s);
            }
            Assert.IsTrue(files.ToArray().Length > 0);
        }
    }
}
