
namespace Com.DaacoWorks.Protocol.Exception
{
    /// <summary>
    ///  Error codes and its error messages constants.
    /// </summary>
    public class ErrorCodes
    {
        /// <summary>
        /// Error code for Invalid connection parameter
        /// </summary>
        public const int INVALID_CONNECTION_PARAMS = 6001;
        /// <summary>
        /// Error code for connection error
        /// </summary>
        public const int CONNECT_ERROR = 6002;
        /// <summary>
        /// Error message for failed connection
        /// </summary>
        public const string CONNECT_ERROR_MSG = "Unable to establish {0} connection for Host='{1}', Port='{2}'";
        /// <summary>
        /// Error message for invalid connection parameter
        /// </summary>
        public const string INVALID_CONNECTION_PARAMS_MSG = "Invalid Connection params ";
        /// <summary>
        /// Error message for cannot read channel
        /// </summary>
        public const string CANNOT_READ_CHANNEL = "Faild to Read from server: connection='{0}' channel with host='{1}' port='{2}";
        /// <summary>
        /// Error message indicating disposal of underlying connection
        /// </summary>
        public const string CONNECTION_DISPOSED_MSG = "Underlying connection is already disposed";
    }

}