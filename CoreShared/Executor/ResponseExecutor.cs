
namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// ResponseExecutor is a executor service which executes the response received.
    /// </summary>
    public class ResponseExecutor : ThreadPoolExecutor, IResponseExecutor
    {

        /// <summary>
        /// Instantiates a new response executor.
        /// </summary>
        /// <param name="numberOfThreads">number of threads to run in parallel</param>
        internal ResponseExecutor(int numberOfThreads) : base(numberOfThreads)
        {
        }

        /// <summary>
        /// Submit response for execution
        /// </summary>
        /// <param name="task"></param>
        public void SubmitResponse(IRunnable task)
        {
            base.Submit(task);
        }
    }
}
