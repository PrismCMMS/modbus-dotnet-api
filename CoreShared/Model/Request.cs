
namespace Com.DaacoWorks.Protocol.Model
{
    /// <summary>
    /// Request represents a request submitted for execution
    /// </summary>
    public abstract class Request : ProtocolDataUnit
    {
        /// <summary>
        /// Gets/sets the request identifier.
        /// </summary>
        public abstract RequestIdentifier RequestIdentifier
        {
            get; set;
        }

        /// <summary>
        /// Gets/Sets whether the request made is scheduled request or not
        /// True represents Scheduled request and false represents either Synchronous or Asynchronous request
        /// </summary>
        public abstract bool IsScheduledRequest { get; set; }
    }
}
