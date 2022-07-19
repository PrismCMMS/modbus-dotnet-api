

namespace Com.DaacoWorks.Protocol.Model
{
    /// <summary>
    /// Socketparameter represents the parameters required for establishing TCP/UDP connection.
    /// </summary>
    public class SocketParameters : ConnectionParameters
    {
        /// <summary>
        /// Instantiates a new socket parameters.
        /// </summary>
        /// <param name="hostName">the host name</param>
        /// <param name="port">port number</param>
        /// <param name="type">connection type</param>
        public SocketParameters(string hostName, int port, ConnectionType type) : base(hostName, port, type)
        {

        }
    }
}