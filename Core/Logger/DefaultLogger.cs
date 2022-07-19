using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Com.DaacoWorks.Protocol.Logger
{
    /// <summary>
    /// Default logger is used when log4net and Enterpriselibrary logger
    /// are not used by the application.
    /// </summary>
    internal class DefaultLogger : ILogger {

        private static TraceSource traceSource =
            new TraceSource("ModbusStack");

        /// <summary>
        /// Instantiates a new default logger.
        /// </summary>
        /// <param name="name"></param>
        public DefaultLogger(string name) {
            
        }

        
        public void Info(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "") {
            traceSource.TraceInformation(LoggerFactory.GetTraceHeader(className, memberName) + msg);
            traceSource.Flush();
        }

        public void Debug(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "") {
            traceSource.TraceData(TraceEventType.Verbose, 0, LoggerFactory.GetTraceHeader(className, memberName) + msg);
            traceSource.Flush();
        }

        
        public void Warn(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "") {
            traceSource.TraceData(TraceEventType.Warning, 0, LoggerFactory.GetTraceHeader(className, memberName) + msg);
            traceSource.Flush();
        }

        
        public void Error(string msg, System.Exception exception, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "") {
            traceSource.TraceData(TraceEventType.Critical, 0, LoggerFactory.GetTraceHeader(className, memberName) + msg, exception);
            traceSource.Flush();
        }

        
    }
}