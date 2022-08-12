using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Model;
using Com.DaacoWorks.Protocol.Logger;
using System.IO;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    ///  Handler class to handle the incoming bytes read from socket.
    /// </summary>
    /// <typeparam name="TRequest">request for which response is expected</typeparam>
    /// <typeparam name="TSuccess">Success response received from the peer</typeparam>
    /// <typeparam name="TError">Error response received from the peer</typeparam>
    public class ChannelInboundHandler<TRequest, TSuccess, TError> : IResponseHandler
        where TRequest : Request
        where TSuccess : SuccessResponse
        where TError : ErrorResponse
    {
        private static ILogger logger = LoggerFactory.GetLogger(typeof(ChannelInboundHandler<TRequest, TSuccess, TError>).FullName);

        private IDecoder<Response> decoder;
        private ConnectionParameters connectionParameters;

        /// <summary>
        /// Instantiates a new channel inbound handler.
        /// </summary>
        /// <param name="connectionParameters">connection paramaters</param>
        /// <param name="decoder">decoder of the response</param>
        public ChannelInboundHandler(ConnectionParameters connectionParameters, IDecoder<Response> decoder)
        {
            this.connectionParameters = connectionParameters;
            this.decoder = decoder;
        }

        private class ResponseNotifier : IRunnable
        {

            private IDecoder<Response> decoder;
            private byte[] data;
            private ConnectionParameters connectionParameters;
            public ResponseNotifier(ConnectionParameters connectionParameters, byte[] data, IDecoder<Response> decoder)
            {
                this.connectionParameters = connectionParameters;
                this.data = data;
                this.decoder = decoder;
                CancellationToken = CancellationToken.None;
            }

            public CancellationToken CancellationToken
            {
                get;
                private set;
            }

            public void Run()
            {
                if (CancellationToken.CanBeCanceled && CancellationToken.IsCancellationRequested) return;

                Response response = null;
                using (var buffer = new MemoryStream(data))
                {
                    response = decoder.Decode(connectionParameters, buffer);
                }
                if (CancellationToken.CanBeCanceled && CancellationToken.IsCancellationRequested) return;

                var map = RequestMap<TRequest, TSuccess, TError>.GetInstance();
                if (response is SuccessResponse)
                {
                    map.NotifySuccess((TSuccess)response);
                }
                else if (response is ErrorResponse)
                {
                    map.NotifyError((TError)response);
                }
            }
        }

        /// <summary>
        /// Submits response data for processing
        /// </summary>
        /// <param name="data"></param>
        public void HandleReadBytes(byte[] data)
        {
            ExecutorFactory.GetGlobalResponseExecutor().SubmitResponse(new ResponseNotifier(connectionParameters, data, decoder));
        }
    }

}