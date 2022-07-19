
namespace Com.DaacoWorks.Protocol.Exception
{
    /// <summary>
    ///  ConnectionException is a runtime exception thrown when there is some issue encountered 
    ///  while opening a socket connection.
    /// </summary>
    public class ConnectionException : System.Exception
    {
        private int errorCode;

        /// <summary>
        /// Instantiates a new connection exception.
        /// </summary>
        /// <param name="errorCode">error code</param>
        /// <param name="message"></param>
        public ConnectionException(int errorCode, string message) : base(message)
        {
            this.errorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code
        /// </summary>
        /// <returns>error code</returns>
        public int GetErrorCode()
        {
            return errorCode;
        }

    }
}