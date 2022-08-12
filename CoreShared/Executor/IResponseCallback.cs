

using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// IResponseCallback interface represents methods to handle the responses.
    /// </summary>
    /// <typeparam name="TSuccess">Success response from the Peer</typeparam>
    /// <typeparam name="TError">Error  response from the Peer</typeparam>
    public interface IResponseCallback<TSuccess, TError>
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {
        /// <summary>
        /// On success.
        /// </summary>
        /// <param name="response">success response</param>
        void OnSuccess(TSuccess response);

        /// <summary>
        /// On error.
        /// </summary>
        /// <param name="error">error response</param>
        void OnError(TError error);

    }

}