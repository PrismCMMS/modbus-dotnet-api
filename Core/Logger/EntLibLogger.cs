using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Runtime.CompilerServices;

namespace Com.DaacoWorks.Protocol.Logger
{
    /// <summary>
    /// EntLibLogger is wrapper over Enterprise Library logger instance.
    /// </summary>
    public class EntLibLogger : ILogger
    {
        private static bool isLogWriterSet = false;

        /// <summary>
        /// Instantiates a new EntLibLogger.
        /// </summary>
        /// <param name="name"></param>
        public EntLibLogger(string name)
        {
            if (!isLogWriterSet)
            {
                Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(new LogWriterFactory().Create());
                isLogWriterSet = true;
            }
        }

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Info(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(LoggerFactory.GetTraceHeader(className, memberName) + msg, "Information", 4, 0, System.Diagnostics.TraceEventType.Information);
        }

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Debug(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(LoggerFactory.GetTraceHeader(className, memberName) + msg, "Debug", 3, 0, System.Diagnostics.TraceEventType.Verbose);
        }

        /// <summary>
        /// Log warining message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Warn(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(LoggerFactory.GetTraceHeader(className, memberName) + msg, "Warning", 2, 0, System.Diagnostics.TraceEventType.Warning);
        }

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="exception"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Error(string msg, System.Exception exception, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            var data = string.Format("{0} {1}\n{2}", LoggerFactory.GetTraceHeader(className, memberName), msg, exception.ToString());
            Microsoft.Practices.EnterpriseLibrary.Logging.Logger.Write(data, "Error", 1, 0, System.Diagnostics.TraceEventType.Critical);
        }

    }

}
