
namespace Com.DaacoWorks.Protocol.Exception
{
    /// <summary>
    /// Represents Protocol layer Exception 
    /// </summary>
    public class ProtocolException: System.Exception
    {
        /// <summary>
        /// Instantiates ProtocolException
        /// </summary>
        /// <param name="message"></param>
        public ProtocolException(string message): base(message)
        {

        }
    }
}
