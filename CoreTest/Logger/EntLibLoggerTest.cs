using Com.DaacoWorks.Protocol.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using System.IO;
using TestSystem;

namespace CoreTest.Logger
{
    [TestClass]
    public class EntLibLoggerTest
    {
        private string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        [ClassInitialize]
        public static void BeforeAllTestMethods(TestContext context)
        {
            var logFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\entliblog-file.txt";
            if (File.Exists(logFile)) File.Delete(logFile);
        }

       

        [TestMethod]
        [TestCategory("Core\\Logging-EntLibLogger")]
        [Description("Check whether EntLibLogger is able to generate log file")]
        public void Check_If_LogFile_Generates()
        {
            var logFile = basePath + @"\entliblog-file.txt";
            var logger = new EntLibLogger("test");
            logger.Info("hello");
            Assert.IsTrue(File.Exists(logFile));
        }

        [TestMethod]
        [TestCategory("Core\\Logging-EntLibLogger")]
        [Description("Check whether Log4NetLogger calls Info method of log4net")]
        public void Check_Info_Log()
        {
            var logger = new EntLibLogger("test");
            logger.Info("hello");
        }

        [TestMethod]
        [TestCategory("Core\\Logging-EntLibLogger")]
        [Description("Check whether Log4NetLogger calls Debug method of log4net")]
        public void Check_Debug_Log()
        {
            var logger = new EntLibLogger("test");     
            logger.Debug("hello");
            
        }

        [TestMethod]
        [TestCategory("Core\\Logging-EntLibLogger")]
        [Description("Check whether Log4NetLogger calls Warn method of log4net")]
        public void Check_Warn_Log()
        {
            var logger = new EntLibLogger("test");
            logger.Warn("hello");
        }

        [TestMethod]
        [TestCategory("Core\\Logging-EntLibLogger")]
        [Description("Check whether Log4NetLogger calls Warn method of log4net")]
        public void Check_Error_Log()
        {
            var logger = new EntLibLogger("test");            
            logger.Error("hello", new Exception("test"));

            
        }
    }
}
