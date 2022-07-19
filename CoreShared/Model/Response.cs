

namespace Com.DaacoWorks.Protocol.Model
{
    /// <summary>
    /// abstract base class to represent response received from peer.
    /// </summary>
    public abstract class Response: ProtocolDataUnit
    {
        /// <summary>
        /// Gets/Sets the request.
        /// </summary>
        public abstract Request Request
        {
            get; set;
        }
        
    }

}