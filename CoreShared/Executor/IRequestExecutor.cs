
using System;

namespace Com.DaacoWorks.Protocol.Executor
{

    /// <summary>
    ///  IRequestExecutor interface represents methods to execute the requests.
    /// </summary>
    public interface IRequestExecutor {

        /// <summary>
        /// Submits the task for execution
        /// </summary>
        /// <param name="task"></param>
        void Submit(IExecutorTask task);

        /// <summary>
        /// Schedules the task to be executed at a periodic interval.
        /// </summary>
        /// <param name="task">the task</param>
        /// <param name="dueTime">due time</param>
        /// <param name="interval">interval</param>
        void Schedule(IExecutorTask task, TimeSpan dueTime, TimeSpan interval);


    }
}