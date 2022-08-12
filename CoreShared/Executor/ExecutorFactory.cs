namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Factory class to provide single threaded executor service for request and response.
    /// </summary>
    public sealed class ExecutorFactory
    {
        /// <summary>
        /// Gets the global request executor.
        /// </summary>
        /// <returns>the global request executor</returns>
        public static IRequestExecutor GetGlobalRequestExecutor()
        {
            return GlobalExecutor.Instance.GetRequestExecutor();
        }

        /// <summary>
        /// Gets the global response executor.
        /// </summary>
        /// <returns>the global response executor</returns>
        public static IResponseExecutor GetGlobalResponseExecutor()
        {
            return GlobalExecutor.Instance.GetResponseExecutor();
        }
    }
}