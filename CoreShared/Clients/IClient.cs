using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;
using System;

namespace Com.DaacoWorks.Protocol.Clients
{
    /// <summary>
    /// Marker client interface
    /// </summary>
    public interface IClient
    {
        /// <summary>
        /// Shuts down the connection to the peer.
        /// </summary>
        void Shutdown();
    }

    /// <summary>
    /// Client interface defines the basic operations to be supported by the implementors.
    /// </summary>
    /// <typeparam name="TRequest">Request to be executed</typeparam>
    /// <typeparam name="TResponse">Response received from request submitted</typeparam>
    /// <typeparam name="TSuccess">Success response received from the peer</typeparam>
    /// <typeparam name="TError">Error response received from the peer</typeparam>
    public interface IClient<TRequest, TResponse, TSuccess , TError>: IClient
        where TRequest: Request
        where TResponse: Response
        where TSuccess: SuccessResponse
        where TError : ErrorResponse

    {

        /// <summary>
        /// Submits the request and returns the future of response.
        /// </summary>
        /// <param name="requestObject">the request object</param>
        /// <returns>the future object to track response</returns>
        TaskFuture<TRequest, TResponse, TSuccess, TError> Submit(TRequest requestObject);

        /// <summary>
        /// Submits the request with a callback.Response will be notified to callback.
        /// </summary>
        /// <param name="requestObject">the request object</param>
        /// <param name="callBack">the call back method that will be called when execution is completed</param>
        void SubmitAsync(TRequest requestObject, IResponseCallback<TSuccess, TError> callBack);
                	
        /// <summary>
        /// Schedules the request and response will be notified to callback periodically.
        /// </summary>
        /// <param name="requestObject">the request object</param>
        /// <param name="interval">the interval</param>
        /// <param name="callBack">the call back</param>
        void Schedule(TRequest requestObject, TimeSpan interval, IResponseCallback<TSuccess, TError> callBack);

       
    }

}