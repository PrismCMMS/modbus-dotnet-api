using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;
using System.IO;

namespace Com.DaacoWorks.Protocol.Codec
{
    /// <summary>
    /// Encode interface to encode the request object into output buffer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEncoder<T>
        where T : IRequestPDUWrapper
    {
        /// <summary>
        /// Encode the request object into output buffer.
        /// </summary>
        /// <param name="metaInfo">meta information</param>
        /// <param name="output">output as memory stream</param>
        void Encode(T metaInfo, MemoryStream output);
    }
}