using Com.DaacoWorks.Protocol.Model;
using System;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// TaskFuture is a future for the task submitted.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class TaskFuture<TRequest, TResponse, TSuccess, TError>: IDisposable
       where TRequest : Request
        where TResponse : Response
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {



        private TRequest pdu;
        private TaskFutureCallBack<TSuccess, TError> futureCallBack;
        private PDUFutureTask task;
        private CancellationTokenSource cancellationTokenSource;
        private bool isDisposed;

        /// <summary>
        /// Instantiates a new task future.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="pdu"></param>
        /// <param name="futureCallBack"></param>
        /// <param name="cancellationTokenSource"></param>
        public TaskFuture(PDUFutureTask task, TRequest pdu, TaskFutureCallBack<TSuccess, TError> futureCallBack, CancellationTokenSource cancellationTokenSource)
        {
            isDisposed = false;
            this.pdu = pdu;
            this.futureCallBack = futureCallBack;
            this.task = task;
            this.cancellationTokenSource = cancellationTokenSource;            
        }

        /// <summary>
        /// Cancel a running Task
        /// </summary>
        /// <returns>true if cancellation is completed in 100ms</returns>
        public bool Cancel()
        {
            if (isDisposed) return false;
            cancellationTokenSource.Cancel();
            return cancellationTokenSource.Token.WaitHandle.WaitOne(100);
            
        }

        /// <summary>
        /// Check if the task is cancelled
        /// </summary>
        /// <returns>true iof cancelled</returns>
        public bool IsCancelled()
        {
            if (isDisposed) return false;
            return cancellationTokenSource.IsCancellationRequested;
        }

        /// <summary>
        /// Check if the task is completed and response is received
        /// </summary>
        /// <returns>true if response is received</returns>
        public bool IsDone()
        {
            return RequestMap<TRequest, TSuccess, TError>.GetInstance().IsResponseRecieved(pdu);
        }

        /// <summary>
        /// Gets the response received after completion of the task. 
        /// Blocks the thread till task execution completed and response is received
        /// </summary>
        /// <returns>the response</returns>
        public TResponse Get()
        {
            if (isDisposed) return null;
            return (TResponse)futureCallBack.GetResponse();
        }

        /// <summary>
        /// Gets the response received after completion of the task. 
        /// Blocks the thread till the specified timeout time or 
        /// till task execution completed if before timeout and response is received
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns>response</returns>
        public TResponse Get(TimeSpan timeout)
        {
            if (isDisposed) return null;
            return (TResponse)futureCallBack.GetResponse(timeout);
        }

        /// <summary>
        /// Gets the task
        /// </summary>
        /// <returns></returns>
        public IExecutorTask GetTask()
        {
            if (isDisposed) return null;
            return task.GetTask();
        }

        /// <summary>
        /// Disposes the TaskFuture
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            if (cancellationTokenSource !=null)
            {
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
            if(futureCallBack!=null)
            {
                futureCallBack.Dispose();
                futureCallBack = null;
            }
            task = null;
            pdu = null;
            GC.SuppressFinalize(this);
        }
    }

}