using System;
using System.IO;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Handler for data read from socket
    /// </summary>
    /// <param name="bytesRead"></param>
    public delegate void CompletionHandler(int bytesRead);

    /// <summary>
    ///  IChannel interface exposes basic channel operations like open,read, write and close.
    /// </summary>
    interface IChannel : IDisposable
    {
        /// <summary>
        /// Open Channel
        /// </summary>
        void OpenChannel();

        /// <summary>
        /// Read the bytes into byte buffer.
        /// </summary>
        /// <param name="readBuffer">read buffer</param>
        /// <param name="handler">handler to process read data</param>
        void Read(MemoryStream readBuffer, CompletionHandler handler);

        /// <summary>
        /// Writes bytes into socket.
        /// </summary>
        /// <param name="writeBuffer">write buffer</param>
        /// <returns></returns>
        int Write(MemoryStream writeBuffer);

        /// <summary>
        /// Close channel
        /// </summary>
        void CloseChannel();

    }
}