using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttachmentFileScanner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AttachmentFileScanner.Tests
{
    [TestClass()]
    public class FileScannerTests
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get; set;
        }

        [TestMethod()]
        [DeploymentItem("TestFileFolder", "Source")]
        public void StartCheckFileTest()
        {
            var scanner = new FileScanner();

            var okPath = Path.Combine(TestContext.DeploymentDirectory, @"Source\OkFile.txt");
            Assert.AreEqual(FileScanner.ScanResult.Ok, scanner.StartCheckFile(okPath).Result);

            var NotFoundPath = Path.Combine(TestContext.DeploymentDirectory, @"Source\NotFound.txt");
            Assert.AreEqual(FileScanner.ScanResult.NotFoundFile, scanner.StartCheckFile(NotFoundPath).Result);

            var hoge = "hoge";
            Assert.AreEqual(FileScanner.ScanResult.NotFoundFile, scanner.StartCheckFile(hoge).Result);

        }
    }
}