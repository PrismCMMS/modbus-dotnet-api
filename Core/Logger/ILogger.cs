using System;
using System.Runtime.CompilerServices;

namespace Com.DaacoWorks.Protocol.Logger
{
    /// <summary>
    /// ILogger interface represents methods for different log levels.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log information
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        void Info(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "");

        /// <summary>
        /// Log debug information
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        void Debug(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "");

        /// <summary>
        /// Log warning message
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        void Warn(string msg, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "");

        /// <summary>
        /// Log error message
        /// </summary>
        /// <param name="msg">the message</param>
        /// <param name="exception"></param>
        /// <param name="className"></param>
        /// <param name="memberName"></param>
        void Error(string msg, System.Exception exception, [CallerFilePath] string className = "", [CallerMemberName] string memberName = "");
        
    }
}