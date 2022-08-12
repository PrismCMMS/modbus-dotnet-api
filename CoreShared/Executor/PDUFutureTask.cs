
namespace Com.DaacoWorks.Protocol.Executor
{

    /// <summary>
    /// PDUFutureTask defines what a task is
    /// </summary>
    public class PDUFutureTask
    {

        private IExecutorTask task;

        /// <summary>
        /// Instantiates a new PDU Future Task
        /// </summary>
        /// <param name="task">the task</param>
        public PDUFutureTask(IExecutorTask task)
        {
            this.task = task;
        }

        /// <summary>
        /// Gets the task
        /// </summary>
        /// <returns>the task</returns>
        public IExecutorTask GetTask()
        {
            return task;
        }
    }

}