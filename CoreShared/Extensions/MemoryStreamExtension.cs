using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.DaacoWorks.Protocol.Extensions
{
    /// <summary>
    /// An extension class for memory stream
    /// </summary>
    public static class MemoryStreamExtension
    {
        /// <summary>
        /// resets the read position to 0
        /// </summary>
        /// <param name="stream"></param>
        public static void Flip(this MemoryStream stream)
        {
            stream.Position = 0;            
        }

        /// <summary>
        /// Sets the length to 0 and resets the read position to 0
        /// </summary>
        /// <param name="stream"></param>
        public static void Clear(this MemoryStream stream)
        {
            stream.Position = 0;
            stream.SetLength(0);
        }

        /// <summary>
        /// Checks if there are any more bytes to read from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns>true if there are any byte available to read</returns>
        public static bool HasRemaining(this MemoryStream stream)
        {
            return stream.Position < stream.Length;
        }

        /// <summary>
        /// Gets the number of bytes left to be read
        /// </summary>
        /// <param name="stream">memo</param>
        /// <returns>number of bytes left</returns>
        public static int Remaining(this MemoryStream stream)
        {
            return (int)(stream.Length - stream.Position);
        }

        /// <summary>
        /// Reads a signed short value from stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>unsigned short value</returns>
        public static ushort ReadShort(this MemoryStream buffer)
        {
            var highByte = buffer.ReadByte();
            var lowByte = buffer.ReadByte();
            return (ushort)((highByte << 8) | lowByte);
        }

        /// <summary>
        /// reads unsigned short value from stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>unsigned short</returns>
        public static ushort ReadUnsignedShort(this MemoryStream buffer)
        {
            var highByte = buffer.ReadByte();
            var lowByte = buffer.ReadByte();
            return (ushort)((highByte << 8) | lowByte);
        }

        /// <summary>
        /// reads signed int value from stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="data"></param>
        /// <returns>signed int value</returns>
        public static int Read(this MemoryStream buffer, byte[] data)
        {
            return buffer.Read(data, 0, data.Length);
        }

        /// <summary>
        /// writes byte array to stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="data"></param>
        public static void Write(this MemoryStream buffer, byte[] data)
        {
            buffer.Write(data, 0, data.Length);
        }
    }
}
