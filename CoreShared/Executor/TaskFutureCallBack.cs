using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// TaskFutureCallBack is a callback used to notify on success or error responses.
    /// </summary>
    /// <typeparam name="TSuccess">success response received from peer</typeparam>
    /// <typeparam name="TError">error response received from peer</typeparam>
    public class TaskFutureCallBack<TSuccess, TError> : IResponseCallback<TSuccess, TError>, IDisposable
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(TaskFutureCallBack<TSuccess, TError>).FullName);
        private Response response = null;
        private object syncObj = new object();
        private bool isDisposed = false;
        Timer timer = null;

        /// <summary>
        /// Handler for success response
        /// </summary>
        /// <param name="response"></param>
        public void OnSuccess(TSuccess response)
        {
            if (isDisposed) return;

            lock (syncObj)
            {
                this.response = response;
                Monitor.PulseAll(syncObj);
            }
        }

        /// <summary>
        /// Handler for error response
        /// </summary>
        /// <param name="error"></param>
        public void OnError(TError error)
        {
            if (isDisposed) return;
            lock (syncObj)
            {
                this.response = error;
                Monitor.PulseAll(syncObj);
            }
        }

        /// <summary>
        /// Gets the response
        /// </summary>
        /// <returns>the response</returns>
        public Response GetResponse()
        {

            if (isDisposed) return null;

            if (response == null)
            {
                lock (syncObj)
                {
                    if (response == null)
                    {
                        try
                        {
                            Monitor.Wait(syncObj);
                        }
                        catch (ThreadInterruptedException e)
                        {
                            //TODO: avoid suppressing exception}
                            logger.Error("Error at GetResponse()", e);
                        }
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Gets the response if received within defined timeout time else returns null
        /// </summary>
        /// <param name="timeout">timeout time</param>
        /// <returns>response if received within timeout time else null</returns>
        public Response GetResponse(TimeSpan timeout)
        {
            if (isDisposed) return null;

            if (response == null)
            {
                lock (syncObj)
                {
                    if (response == null)
                    {
                        StartTimer(timeout);
                        try
                        {
                            Monitor.Wait(syncObj);
                        }
                        catch (ThreadInterruptedException e)
                        {
                            //TODO: avoid suppressing exception
                            logger.Error("Error at GetResponse(TimeSpan timeout)", e);
                        }
                    }
                }
            }

            return response;
        }

        private void StartTimer(TimeSpan timeout)
        {
            if (timer != null) return;

            timer = new Timer((syncObj) =>
            {
                if (!isDisposed)
                    Monitor.PulseAll(syncObj);
                DisposeTimer();
            }, null, 0, Convert.ToInt32(timeout.TotalMilliseconds));
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        /// <summary>
        /// Dsiposes a Future task
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;
            Monitor.PulseAll(syncObj);
            DisposeTimer();
            GC.SuppressFinalize(this);
        }
    }

}