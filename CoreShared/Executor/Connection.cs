using System;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System.IO;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Util;
using Com.DaacoWorks.Protocol.Exception;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Represents a connection instance
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Shutsdown a connection
        /// </summary>
        void Shutdown();
    }

    /// <summary>
    ///  Connection class which represents socket connection with the peer.
    /// </summary>
    /// <typeparam name="TRequestPDUWrapper">request object to be written to socket</typeparam>
    /// <typeparam name="TResponse">response object transformed from bytes read from socket</typeparam>
    public abstract class Connection<TRequestPDUWrapper, TResponse> : IConnection, IDisposable
        where TRequestPDUWrapper : IRequestPDUWrapper
        where TResponse : Response
    {
        private static ILogger logger = LoggerFactory.GetLogger(typeof(Connection<TRequestPDUWrapper, TResponse>).FullName);
        private MemoryStream writeBuffer = null;
        private MemoryStream readBuffer = null;
        private IChannel channel;
        private CompletionHandler handler;
        private ConnectionParameters connectionParams;
        private bool isDisposed;

        /// <summary>
        /// Instantiates new connection
        /// </summary>
        /// <param name="connectionParams">connection parameters</param>
        public Connection(ConnectionParameters connectionParams)
        {
            isDisposed = false;
            ValidateConnectionParams(connectionParams);
            this.connectionParams = connectionParams;
            channel = ChannelFactory.GetChannel(connectionParams);
            writeBuffer = new MemoryStream(GetWriteBufferSize());
            readBuffer = new MemoryStream(GetReadBufferSize());
            handler = InvokeResponseHandler;
            channel.OpenChannel();
            channel.Read(readBuffer, handler);
        }

        /// <summary>
        /// Gets the connection parameters.
        /// </summary>
        /// <returns></returns>
        protected ConnectionParameters GetConnectionParameters()
        {
            return connectionParams;
        }

        private void ValidateConnectionParams(ConnectionParameters connectionParams)
        {
            if (connectionParams == null || connectionParams.GetHost() == null ||
                    connectionParams.GetPort() < 1 && connectionParams.GetPort() > 65535)
            {
                throw new ConnectionException(ErrorCodes.INVALID_CONNECTION_PARAMS, ErrorCodes.INVALID_CONNECTION_PARAMS_MSG);
            }
        }


        private void InvokeResponseHandler(int bytesRead)
        {
            if (isDisposed) return;

            readBuffer.Flip();

            byte[] data = new byte[bytesRead];
            readBuffer.Read(data);
            var responseHandler = ResponseHandler();
            if (responseHandler != null)
                responseHandler.HandleReadBytes(data);
            readBuffer.Flip();
            logger.Debug("Bytes read " + ProtocolUtils.GetHexByteString(readBuffer));
            readBuffer.Clear();
            channel.Read(readBuffer, handler);
        }

        /// <summary>
        /// Write data to the socket
        /// </summary>
        /// <param name="pduWrapper">the pdu wrapper</param>
        public void Write(TRequestPDUWrapper pduWrapper)
        {
            if (isDisposed)
                throw new ProtocolException(ErrorCodes.CONNECTION_DISPOSED_MSG);

            channel.OpenChannel();
            GetEncoder().Encode(pduWrapper, writeBuffer);

            writeBuffer.Flip();
            channel.Write(writeBuffer);
            writeBuffer.Flip();
            logger.Debug("Bytes written " + ProtocolUtils.GetHexByteString(writeBuffer));
            writeBuffer.Clear();
        }

        /// <summary>
        /// Shutdown the connection and close underlying channel
        /// </summary>
        public void Shutdown()
        {
            try
            {
                channel.CloseChannel();
            }
            catch (System.Exception e)
            {
                logger.Error("Exception while shutdown the Channel ", e);
            }
        }

        /// <summary>
        /// Response handler
        /// </summary>
        /// <returns>response handler</returns>
        protected abstract IResponseHandler ResponseHandler();

        /// <summary>
        /// gets the encoder
        /// </summary>
        /// <returns>encoder</returns>
        protected abstract IEncoder<TRequestPDUWrapper> GetEncoder();

        /// <summary>
        /// Gets Write buffer size
        /// </summary>
        /// <returns>size of write buffer as integer</returns>
        protected abstract int GetWriteBufferSize();

        /// <summary>
        /// Gets Read buffer size
        /// </summary>
        /// <returns>size of read buffer as integer</returns>
        protected abstract int GetReadBufferSize();

        /// <summary>
        /// Disposes the connection by closing the underlying channel 
        /// and frees all acquired resources
        /// </summary>
        public void Dispose()
        {
            isDisposed = true;

            if (channel != null)
            {
                Shutdown();
                channel.Dispose();
                channel = null;
            }
            if (readBuffer != null)
            {
                readBuffer.Dispose();
                readBuffer = null;
            }

            if (writeBuffer != null)
            {
                writeBuffer.Dispose();
                writeBuffer = null;
            }
            
            GC.SuppressFinalize(this);
        }
    }

}