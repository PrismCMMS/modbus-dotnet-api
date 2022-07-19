using System;
using System.Threading;

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
            ILogger logger = null;

            try
            {
                logger = new EntLibLogger(name);

            }
            catch
            {
                try
                {
                    logger = new Log4NetLogger(name);

                }
                catch
                {
                    logger = new DefaultLogger(name);
                }
            }

            return logger;
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