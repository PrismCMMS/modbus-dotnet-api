using Com.DaacoWorks.Protocol.Model;
using Com.DaacoWorks.Protocol.Util;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// IRequestPDUWrapper interface to represent Request PDU Wrapper
    /// </summary>
    public interface IRequestPDUWrapper
    {
    }

    /// <summary>
    /// RequestPDUWrapper wraps the request, callback and associated generated requestId.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TSuccess"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public abstract class RequestPDUWrapper<TRequest, TSuccess, TError>: IRequestPDUWrapper     
        where TRequest: Request
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {
       
        /// <summary>
        /// Instantiates a new request PDU wrapper.
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="pdu"></param>
        public RequestPDUWrapper(IResponseCallback<TSuccess, TError> callBack, TRequest pdu)
        {
            Time = ProtocolUtils.CurrentTimeMillis();
            this.CallBack = callBack;
            this.Pdu = pdu;
        }

        /// <summary>
        /// Gets/Sets the time
        /// </summary>
        public long Time
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets/Sets the callback which intimates success or failure of request
        /// </summary>
        public IResponseCallback<TSuccess, TError> CallBack
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets/Sets the PDU
        /// </summary>
        public TRequest Pdu
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets Request Id
        /// </summary>
        /// <returns>the request id</returns>
        public RequestIdentifier GetRequestId()
        {
            return Pdu.RequestIdentifier;
        }

        /// <summary>
        /// Gets the timeout error
        /// </summary>
        /// <returns></returns>
        public abstract TError GetTimeoutError();

    }
}
