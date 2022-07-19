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
    /// UDPChannel represents the UDP connection with a peer.
    /// </summary>
    internal class UDPChannel : IChannel
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(UDPChannel).FullName);

        private UdpClient udpClient;
        private ConnectionParameters connectionParams;
        private CancellationTokenSource tokenSource;
        private bool isDisposed;

        /// <summary>
        /// Instantiates a new UDP channel.
        /// </summary>
        /// <param name="connectionParams">connection parameters</param>
        public UDPChannel(ConnectionParameters connectionParams)
        {
            isDisposed = false;
            this.connectionParams = connectionParams;
        }


        public void OpenChannel()
        {
            try
            {
                if (udpClient == null)
                {
                    if (isDisposed)
                        throw new ProtocolException("UDP Channel already shutdown");
                    udpClient = new UdpClient(connectionParams.GetHost(), connectionParams.GetPort());
                    tokenSource = new CancellationTokenSource();
                }
                else
                {
                    udpClient.Connect(connectionParams.GetHost(), connectionParams.GetPort());
                    tokenSource = new CancellationTokenSource();
                }
            }
            catch (System.Exception e)
            {
                var msg = string.Format(ErrorCodes.CONNECT_ERROR_MSG, ConnectionType.UDP, connectionParams.GetHost(), connectionParams.GetPort());
                logger.Error(msg, e);
                throw new ConnectionException(ErrorCodes.CONNECT_ERROR, msg);
            }
        }


        public void CloseChannel()
        {
            tokenSource.Cancel();
            tokenSource.Dispose();
            tokenSource = null;
            udpClient.Close();
        }

        private bool IsTaskCancelled()
        {
            if (tokenSource == null) return true;
            return tokenSource.Token.IsCancellationRequested;
        }

        public int Write(MemoryStream writeBuffer)
        {
            var bytes = writeBuffer.ToArray();
            return udpClient.Send(bytes, bytes.Length);
        }


        public void Read(MemoryStream readBuffer, CompletionHandler handler)
        {
            if (IsTaskCancelled()) return;

            var readTask = udpClient.ReceiveAsync();
            readTask.ContinueWith((taskCompleted) =>
            {
                int bytesRead = 0;
                if (taskCompleted.IsFaulted)
                {
                    logger.Error(string.Format(ErrorCodes.CANNOT_READ_CHANNEL, ConnectionType.UDP, connectionParams.GetHost(), connectionParams.GetPort()), taskCompleted.Exception);
                    CloseChannel();
                }
                else
                {
                    if (IsTaskCancelled()) return;
                    bytesRead = taskCompleted.Result.Buffer.Length;
                    readBuffer.Write(taskCompleted.Result.Buffer, (int)readBuffer.Position, bytesRead);
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

            if (udpClient != null)
            {

                ((IDisposable)udpClient).Dispose();

                udpClient = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
