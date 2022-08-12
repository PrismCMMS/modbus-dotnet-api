using Com.DaacoWorks.Protocol.Model;
using System.IO;

namespace Com.DaacoWorks.Protocol.Codec
{
    /// <summary>
    /// Decoder interface to decode the incoming bytes.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IDecoder<TResponse> 
        where TResponse : Response
    {
        /// <summary>
        /// Decode the bytes available in the input buffer
        /// </summary>
        /// <param name="connectionParameters">the connection parameters</param>
        /// <param name="input">input as memory stream</param>
        /// <returns></returns>
        TResponse Decode(ConnectionParameters connectionParameters, MemoryStream input);
    }
}