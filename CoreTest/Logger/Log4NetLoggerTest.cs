using Com.DaacoWorks.Protocol.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using System;
using TestSystem;
using System.IO;


namespace CoreTest.Logger
{
    [TestClass]
    public class Log4NetLoggerTest
    {
        private string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        

        [ClassInitialize]
        public static void BeforeAllTestMethods(TestContext context)
        {
            var logFile = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\log4net-file.txt";
            if (File.Exists(logFile)) File.Delete(logFile);
        }
        

        [TestMethod]
        [TestCategory("Core\\Logging-Log4NetLogger")]
        [Description("Check whether Log4NetLogger is able to generate log file")]
        public void Check_If_LogFile_Generated()
        {
            var logFile = basePath + @"\log4net-file.txt";
            var logger = new Log4NetLogger("test");
            logger.Info("hello");
            Assert.IsTrue(File.Exists(logFile));
        }

        [TestMethod]
        [TestCategory("Core\\Logging-Log4NetLogger")]
        [Description("Check whether Log4NetLogger calls Info method of log4net")]
        public void Check_Info_Log()
        {
            var loggerMock = MockRepository.GenerateStub<log4net.ILog>();
            var logger = new Log4NetLogger("test");
            logger.SetPrivateFieldValue("logger", loggerMock);

            logger.Info("hello");

            loggerMock.AssertWasCalled(logr => logr.Info(Arg<string>.Is.Anything));
            
        }

        [TestMethod]
        [TestCategory("Core\\Logging-Log4NetLogger")]
        [Description("Check whether Log4NetLogger calls Debug method of log4net")]
        public void Check_Debug_Log()
        {
            var loggerMock = MockRepository.GenerateStub<log4net.ILog>();
            var logger = new Log4NetLogger("test");
            logger.SetPrivateFieldValue("logger", loggerMock);

            logger.Debug("hello");

            loggerMock.AssertWasCalled(logr => logr.Debug(Arg<string>.Is.Anything));
        }

        [TestMethod]
        [TestCategory("Core\\Logging-Log4NetLogger")]
        [Description("Check whether Log4NetLogger calls Warn method of log4net")]
        public void Check_Warn_Log()
        {
            var loggerMock = MockRepository.GenerateStub<log4net.ILog>();
            var logger = new Log4NetLogger("test");
            logger.SetPrivateFieldValue("logger", loggerMock);

            logger.Warn("hello");

            loggerMock.AssertWasCalled(logr => logr.Warn(Arg<string>.Is.Anything));
        }

        [TestMethod]
        [TestCategory("Core\\Logging-Log4NetLogger")]
        [Description("Check whether Log4NetLogger calls Warn method of log4net")]
        public void Check_Error_Log()
        {
            var loggerMock = MockRepository.GenerateStub<log4net.ILog>();
            var logger = new Log4NetLogger("test");
            logger.SetPrivateFieldValue("logger", loggerMock);

            logger.Error("hello", new Exception("test"));

            loggerMock.AssertWasCalled(logr => logr.Error(Arg<string>.Is.Anything, Arg<Exception>.Is.Anything));
        }
    }
}
