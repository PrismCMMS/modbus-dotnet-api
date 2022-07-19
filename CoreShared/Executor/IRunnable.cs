using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Represents a runnable task
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// Cancellation token
        /// </summary>
        CancellationToken CancellationToken { get; }

        /// <summary>
        /// Runs the task
        /// </summary>
        void Run();
    }
}
