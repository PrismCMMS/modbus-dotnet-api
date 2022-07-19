using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using Com.DaacoWorks.Protocol.Util;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{

    /// <summary>
    /// RequestMap is a singleton instance which caches the request information until either the response is received
    ///  for the request or evictor thread removes it after a given REQUEST_TIME_OUT(default is 5000 milliseconds) is reached.
    ///  
    /// RequestMap will call either onSuccess or onError methods of the response callback depends on the response received for a request.
    /// </summary>
    /// <typeparam name="TRequest">Request that was submitted</typeparam>
    /// <typeparam name="TSuccess">Success response received from Peer</typeparam>
    /// <typeparam name="TError">Error response received from Peer</typeparam>
    public class RequestMap<TRequest, TSuccess, TError>
        where TRequest : Request
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {

        //TODO:Can we rename it ro RequestTracker as I feel RequestMap is doing more than just maintaining the mapping.
        //It also notifies the success/failure

        private const long serialVersionUID = -700602279541124764L;
        private static ILogger logger = LoggerFactory.GetLogger(typeof(RequestMap<TRequest, TSuccess, TError>).FullName);

        private static RequestMap<TRequest, TSuccess, TError> requestMap = new RequestMap<TRequest, TSuccess, TError>();

        private ConcurrentDictionary<RequestIdentifier, RequestPDUWrapper<TRequest, TSuccess, TError>> pduMetaInfoMap;

        /// <summary>
        /// Request time out
        /// </summary>
        public static readonly int requestTimeout = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("REQUEST_TIME_OUT")) ?
                                               5000 : int.Parse(Environment.GetEnvironmentVariable("REQUEST_TIME_OUT")) * 1000;

        private RequestMap()
        {
            pduMetaInfoMap = new ConcurrentDictionary<RequestIdentifier, RequestPDUWrapper<TRequest, TSuccess, TError>>();
            ExecutorFactory.GetGlobalRequestExecutor().Schedule(new EntryEvictor(pduMetaInfoMap), TimeSpan.Zero, new TimeSpan(0, 3, 0));
        }

        /// <summary>
        /// Gets the single instance of RequestMap.
        /// </summary>
        /// <returns></returns>
        public static RequestMap<TRequest, TSuccess, TError> GetInstance()
        {
            return requestMap;
        }

        /// <summary>
        /// Notifies Success Response
        /// </summary>
        /// <param name="response">success response</param>
        public void NotifySuccess(TSuccess response)
        {
            RequestIdentifier requestId = response.Request.RequestIdentifier;
            if (pduMetaInfoMap.ContainsKey(requestId))
            {
                try
                {
                    if (pduMetaInfoMap.TryGetValue(requestId, out RequestPDUWrapper<TRequest, TSuccess, TError> requestWrapper))
                    {
                        if (!requestWrapper.Pdu.IsScheduledRequest)
                            pduMetaInfoMap.TryRemove(requestId, out requestWrapper);
                        response.Request = requestWrapper.Pdu;
                        requestWrapper.CallBack.OnSuccess(response);
                    }
                }
                catch (System.Exception e)
                {
                    logger.Info("RequestMap.NotifySuccess -> Exception while clearing internal request cache " + e);
                }
            }
            else
            {
                logger.Info("Response is received for the request Id " + requestId + ". Either the request is cancelled or not found.");
            }
        }

        /// <summary>
        /// Notifies Error Response
        /// </summary>
        /// <param name="errorResp">error response</param>
        public void NotifyError(TError errorResp)
        {
            RequestIdentifier requestId = errorResp.Request.RequestIdentifier;
            if (pduMetaInfoMap.ContainsKey(requestId))
            {
                try
                {
                    if (pduMetaInfoMap.TryGetValue(requestId, out RequestPDUWrapper<TRequest, TSuccess, TError> requestWrapper))
                    {
                        if (!requestWrapper.Pdu.IsScheduledRequest)
                            pduMetaInfoMap.TryRemove(requestId, out requestWrapper);
                        errorResp.Request = requestWrapper.Pdu;
                        requestWrapper.CallBack.OnError(errorResp);
                    }
                }
                catch (System.Exception e)
                {
                    logger.Info("RequestMap.NotifyError -> Exception while clearing internal request cache " + e);
                }
            }
            else
            {
                logger.Info("Response is received for the request Id " + requestId + ". Either the request is cancelled or not found.");
            }

        }

        /// <summary>
        /// Checks if is response recieved.
        /// </summary>
        /// <param name="pdu">the PDU</param>
        /// <returns>true if response received</returns>
        public bool IsResponseRecieved(ProtocolDataUnit pdu)
        {
            foreach (var info in pduMetaInfoMap.Values)
            {
                if (pdu == info.Pdu) return false;
            }
            return true;
        }

        /// <summary>
        /// Gets the request PDU
        /// </summary>
        /// <param name="requestId">request id</param>
        /// <returns>the request PDU</returns>
        public TRequest GetRequestPDU(RequestIdentifier requestId)
        {
            return (TRequest)pduMetaInfoMap[requestId].Pdu;
        }

        /// <summary>
        /// Checks if map is empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return pduMetaInfoMap.IsEmpty;
        }

        private class EntryEvictor : ExecutorTask<TRequest, TSuccess, TError>
        {

            private ConcurrentDictionary<RequestIdentifier, RequestPDUWrapper<TRequest, TSuccess, TError>> pduMetaInfoMap;

            internal EntryEvictor(ConcurrentDictionary<RequestIdentifier, RequestPDUWrapper<TRequest, TSuccess, TError>> pduMetaInfoMap) : base(null, null, null, CancellationToken.None)
            {
                this.pduMetaInfoMap = pduMetaInfoMap;
            }
            
            internal EntryEvictor(ConcurrentDictionary<RequestIdentifier, RequestPDUWrapper<TRequest, TSuccess, TError>> pduMetaInfoMap, TRequest pdu, IResponseCallback<TSuccess, TError> callBack, IConnection connection) : base(pdu, callBack, connection, CancellationToken.None)
            {
                this.pduMetaInfoMap = pduMetaInfoMap;

            }

            protected override void InnerRun()
            {
                foreach (RequestPDUWrapper<TRequest, TSuccess, TError> info in pduMetaInfoMap.Values)
                {
                    long requestTime = info.Time;
                    if (ProtocolUtils.CurrentTimeMillis() - requestTime > requestTimeout)
                    {
                        if (pduMetaInfoMap.TryRemove(info.GetRequestId(), out RequestPDUWrapper<TRequest, TSuccess, TError> request))
                        {
                            info.CallBack.OnError(info.GetTimeoutError());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds the request PDU meta info.
        /// </summary>
        /// <param name="metaInfo"></param>
        public void AddRequestPDUMetaInfo(RequestPDUWrapper<TRequest, TSuccess, TError> metaInfo)
        {
            pduMetaInfoMap.TryAdd(metaInfo.GetRequestId(), metaInfo);
        }

        /// <summary>
        /// Removes the request PDU meta info.
        /// </summary>
        /// <param name="requestId"></param>
        public void RemoveRequestPDUMetaInfo(RequestIdentifier requestId)
        {
            RequestPDUWrapper<TRequest, TSuccess, TError> metaInfo;
            pduMetaInfoMap.TryRemove(requestId, out metaInfo);
        }

    }

}