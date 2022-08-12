namespace Com.DaacoWorks.Protocol.Model
{
    /// <summary>
    /// Abstract base class for any protocol to represent the protocol data unit (pdu)
    /// </summary>
    public abstract class ProtocolDataUnit
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <returns>the length</returns>
        public abstract int GetLength();

    }

}