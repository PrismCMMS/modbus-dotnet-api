using System.Threading;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Represents an executor task
    /// </summary>
    public interface IExecutorTask : IRunnable
    {
    }

    /// <summary>
    /// ExecutorTask is a wrapper of request to be executed, callback to be called back after receiving response and connection into which request to be written.
    /// </summary>
    /// <typeparam name="TRequest">request to be executed</typeparam>
    /// <typeparam name="TSuccess">success response from peer</typeparam>
    /// <typeparam name="TError">error response from peer</typeparam>
    public abstract class ExecutorTask<TRequest, TSuccess, TError> : IExecutorTask
        where TRequest : ProtocolDataUnit
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {
        /// <summary>
        /// Request Protocol Data Unit
        /// </summary>
        protected TRequest pdu;
        /// <summary>
        /// Response callback object handling success and failures
        /// </summary>
        protected IResponseCallback<TSuccess, TError> callBack;
        /// <summary>
        /// connection object
        /// </summary>
        protected IConnection connection;

        /// <summary>
        /// Instantiates a new ExecutorTask.
        /// </summary>
        /// <param name="pdu"></param>
        /// <param name="callBack"></param>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        public ExecutorTask(TRequest pdu, IResponseCallback<TSuccess, TError> callBack, IConnection connection, CancellationToken cancellationToken)
        {
            this.pdu = pdu;
            this.callBack = callBack;
            this.connection = connection;
            this.CancellationToken = cancellationToken;
        }

        /// <summary>
        /// Cancellation token
        /// </summary>
        public CancellationToken CancellationToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs the task
        /// </summary>
        public void Run()
        {
            if (CancellationToken.CanBeCanceled && CancellationToken.IsCancellationRequested)
                return;

            InnerRun();
        }

        /// <summary>
        /// Abstract inner run which is to be implemented by derived class
        /// </summary>
        protected abstract void InnerRun();
    }
}