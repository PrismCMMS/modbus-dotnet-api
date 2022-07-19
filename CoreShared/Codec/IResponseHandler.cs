namespace Com.DaacoWorks.Protocol.Codec
{
    /// <summary>
    /// interface to handle the incoming bytes.
    /// </summary>
    public interface IResponseHandler {

        /// <summary>
        /// Handle read bytes.
        /// </summary>
        /// <param name="data">the data</param>
        void HandleReadBytes(byte[] data);

    }
}