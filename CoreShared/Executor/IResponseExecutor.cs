using Com.DaacoWorks.Protocol.Executor;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// IResponseExecutor interface represents a method to execute the response.
    /// </summary>
    public interface IResponseExecutor
    {
        /// <summary>
        /// Submit response
        /// </summary>
        /// <param name="task">the task</param>
        void SubmitResponse(IRunnable task);
    }
}
