using Com.DaacoWorks.Protocol.Model;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{

    /// <summary>
    /// Factory class to create ExecutorTask and TaskFutures.
    /// </summary>
    /// <typeparam name="TRequest">request to be executed</typeparam>
    /// <typeparam name="TSuccess">success response received from peer</typeparam>
    /// <typeparam name="TError">error response received from peer</typeparam>
    public abstract class TaskFactory<TRequest, TSuccess, TError>
        where TRequest : Request
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {

        /// <summary>
        /// Gets the Executor task
        /// </summary>
        /// <param name="requestObject">the request object</param>
        /// <param name="callBack">the callback</param>
        /// <param name="connection">the connection</param>
        /// <returns></returns>
        public ExecutorTask<TRequest, TSuccess, TError> GetTask(TRequest requestObject, IResponseCallback<TSuccess, TError> callBack, IConnection connection)
        {
            return GetRunnableTask(requestObject, callBack, connection, CancellationToken.None);
        }

        /// <summary>
        /// Gets the task future
        /// </summary>
        /// <typeparam name="TResponse">response</typeparam>
        /// <param name="requestObject">the request object</param>
        /// <param name="callBack">the callback</param>
        /// <param name="connection">the connection</param>
        /// <returns></returns>
        public TaskFuture<TRequest, TResponse, TSuccess, TError> GetTaskFuture<TResponse>(TRequest requestObject, TaskFutureCallBack<TSuccess, TError> callBack, IConnection connection)
           where TResponse : Response
        {
            var cancellationSource = new CancellationTokenSource();
            ExecutorTask<TRequest, TSuccess, TError> task = GetRunnableTask(requestObject, callBack, connection, cancellationSource.Token);
            return new TaskFuture<TRequest, TResponse, TSuccess, TError>(new PDUFutureTask(task), requestObject, callBack, cancellationSource);
        }

        /// <summary>
        /// Gets the runnable task
        /// </summary>
        /// <param name="requestObject">the request object</param>
        /// <param name="callBack">the callback</param>
        /// <param name="connection">the connection</param>
        /// <param name="token">cancellation token</param>
        /// <returns></returns>
        public abstract ExecutorTask<TRequest, TSuccess, TError> GetRunnableTask(TRequest requestObject, IResponseCallback<TSuccess, TError> callBack, IConnection connection, CancellationToken token);

    }
}