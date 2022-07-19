

using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Headers
{
    /// <summary>
    ///  Header represents the header present in a protocol data unit.
    /// </summary>
    public abstract class Header
    {
        /// <summary>
        /// Protocol Data Unit
        /// </summary>
        protected ProtocolDataUnit pdu;

        /// <summary>
        /// Instantiates a new header.
        /// </summary>
        /// <param name="pdu"></param>
        public Header(ProtocolDataUnit pdu)
        {
            this.pdu = pdu;
        }

        /// <summary>
        /// Gets the header in bytes.
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetHeaderInBytes();       
    }

}