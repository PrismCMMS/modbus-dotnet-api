
using System;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly: InternalsVisibleTo("CoreTest")]

namespace Com.DaacoWorks.Protocol.Logger
{
    /// <summary>
    /// Factory class to provide logger instance.
    /// </summary>
    public class LoggerFactory
    {
        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILogger GetLogger(string name)
        {
           return new DefaultLogger(name);
        }

        /// <summary>
        /// Gets the trace header.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static String GetTraceHeader(string className, string methodName)
        {
            return Thread.CurrentThread.Name + "->" + className + "->" + methodName + "->";
        }

    }
}

