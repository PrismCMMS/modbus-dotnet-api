using log4net;
using System.Runtime.CompilerServices;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Com.DaacoWorks.Protocol.Logger
{
    /// <summary>
    /// Log4NetLogger is wrapper over Log4Net logger instance.
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        ILog logger = null;

        /// <summary>
        /// Instantiates a new log 4 net logger.
        /// </summary>
        /// <param name="name">the name</param>
        public Log4NetLogger(string name)
        {
            logger = LogManager.GetLogger(name);
        }

        /// <summary>
        /// Log information message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Info(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            logger.Info(LoggerFactory.GetTraceHeader(className, memberName) + msg);
        }

        /// <summary>
        /// Log debug message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Debug(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            logger.Debug(LoggerFactory.GetTraceHeader(className, memberName) + msg);
        }

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        public void Warn(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "")
        {
            logger.Warn(LoggerFactory.GetTraceHeader(className, memberName) + msg);
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
            logger.Error(msg, exception);
        }

    }
}