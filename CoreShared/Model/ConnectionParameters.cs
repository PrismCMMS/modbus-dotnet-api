

namespace Com.DaacoWorks.Protocol.Model
{
    /// <summary>
    ///  ConnectionParameters represents the parameters (like host, port) required for 
    ///  establishing a socket connection.
    /// </summary>
    public abstract class ConnectionParameters
    {
        private string host;
        private int port;
        private ConnectionType connectionType;

        /// <summary>
        /// Instantiates a new connection parameters.
        /// </summary>
        /// <param name="host">the hostname</param>
        /// <param name="port">port number</param>
        /// <param name="type">connection type</param>
        public ConnectionParameters(string host, int port, ConnectionType type)
        {
            this.host = host;
            this.port = port;
            this.connectionType = type;
        }

        /// <summary>
        /// Gets the host
        /// </summary>
        /// <returns>the host name</returns>
        public string GetHost()
        {
            return host;
        }

        /// <summary>
        /// Gets the port
        /// </summary>
        /// <returns>port number</returns>
        public int GetPort()
        {
            return port;
        }

        /// <summary>
        /// Gets the connection type
        /// </summary>
        /// <returns>connection type</returns>
        public ConnectionType GetConnectionType()
        {
            return connectionType;
        }

        /// <summary>
        /// checks if objects are equal
        /// </summary>
        /// <param name="obj">any object</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ConnectionParameters)) return false;

            ConnectionParameters sockParam = (ConnectionParameters)obj;

            return this.host == sockParam.GetHost() &&
                   this.port == sockParam.GetPort() &&
                   this.connectionType == sockParam.GetConnectionType();
        }

        /// <summary>
        /// Gets has code
        /// </summary>
        /// <returns>has code</returns>
        public override int GetHashCode()
        {
            return this.host.GetHashCode() | this.port.GetHashCode() | this.connectionType.GetHashCode();
        }

    }
}