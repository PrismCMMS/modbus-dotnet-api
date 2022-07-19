using Com.DaacoWorks.Protocol.Exception;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// TCPChannel represents the TCP connection with a peer.
    /// </summary>
    internal class TCPChannel : IChannel
    {
        private static ILogger logger = LoggerFactory.GetLogger(typeof(TCPChannel).FullName);
        private ConnectionParameters connectionParams;
        private TcpClient tcpClient;
        private CancellationTokenSource tokenSource;
        private bool isDisposed;

        /// <summary>
        /// Instantiates a new TCP channel.
        /// </summary>
        /// <param name="connectionParams">connection parameters</param>
        public TCPChannel(ConnectionParameters connectionParams)
        {
            isDisposed = false;
            this.connectionParams = connectionParams;
        }

        public void OpenChannel()
        {
            try
            {
                if (tcpClient == null)
                {
                    if (isDisposed)
                        throw new ProtocolException("TCP Channel already shutdown");
                    //TODO: replace this commented code properly
                    //socketChannel = AsynchronousSocketChannel.open(GlobalExecutor.Instance.ChannelGroup);
                    tcpClient = new TcpClient(connectionParams.GetHost(), connectionParams.GetPort());
                    tokenSource = new CancellationTokenSource();
                }
                else if (!tcpClient.Connected)
                {
                    tcpClient.Connect(connectionParams.GetHost(), connectionParams.GetPort());
                    tokenSource = new CancellationTokenSource();
                }
            }
            catch (System.Exception e)
            {
                var msg = string.Format(ErrorCodes.CONNECT_ERROR_MSG, ConnectionType.TCP, connectionParams.GetHost(), connectionParams.GetPort());
                logger.Error(msg, e);
                throw new ConnectionException(ErrorCodes.CONNECT_ERROR, msg);
            }
        }



        public void CloseChannel()
        {
            if (IsChannelOpen())
            {
                //TODO: socketChannel.Close()
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = null;
                tcpClient.GetStream().Close();
                tcpClient.Close();
            }
        }

        private bool IsChannelOpen()
        {
            if (tcpClient != null) return tcpClient.Connected;
            return false;
        }


        public int Write(MemoryStream writeBuffer)
        {
            OpenChannel();
            var stream = tcpClient.GetStream();
            var bytes = writeBuffer.ToArray();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            return bytes.Length;
        }

        private bool IsTaskCancelled()
        {
            if (tokenSource == null) return true;
            return tokenSource.Token.IsCancellationRequested;
        }

        private CancellationToken GetCancellationToken()
        {
            if (tokenSource == null) return CancellationToken.None;
            return tokenSource.Token;
        }

        public void Read(MemoryStream readBuffer, CompletionHandler handler)
        {

            if (IsTaskCancelled() || readBuffer.Capacity <= 0) return;

            OpenChannel();

            var stream = tcpClient.GetStream();
            var readBufferSize = readBuffer.Capacity;
            var bytes = new byte[readBufferSize];

            var task = stream.ReadAsync(bytes, 0, bytes.Length, GetCancellationToken());
            task.ContinueWith((taskCompleted) =>
            {
                var bytesRead = 0;
                if (taskCompleted.IsFaulted)
                {
                    logger.Error(string.Format(ErrorCodes.CANNOT_READ_CHANNEL, ConnectionType.TCP, connectionParams.GetHost(), connectionParams.GetPort()), taskCompleted.Exception);
                    CloseChannel();
                }
                else
                {
                    try
                    {
                        bytesRead = taskCompleted.Result;
                        if (!IsTaskCancelled() && bytesRead > 0)
                        {
                            readBuffer.Write(bytes, 0, bytesRead);                            
                        }
                    }
                    catch (System.Exception ex)
                    {
                        bytesRead = 0;
                        logger.Error(string.Format(ErrorCodes.CANNOT_READ_CHANNEL, connectionParams.GetHost(), connectionParams.GetPort()), ex);
                    }
                }

                if (!IsTaskCancelled() && bytesRead > 0)
                    handler(bytesRead);
            });


        }

        public void Dispose()
        {
            isDisposed = true;
            if (tokenSource != null)
            {
                tokenSource.Dispose();
                tokenSource = null;
            }

            if (tcpClient != null)
            {

                ((IDisposable)tcpClient).Dispose();

                tcpClient = null;
            }

            GC.SuppressFinalize(this);
        }

    }
}