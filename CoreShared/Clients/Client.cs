using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Clients
{
    /// <summary>
    /// abstract class to initialize the client instance.
    /// </summary>
    public abstract class Client
    {
        /// <summary>
        /// connection
        /// </summary>
        protected IConnection connection;

        /// <summary>
        /// connection parameters
        /// </summary>
        protected ConnectionParameters connectionParameters;

        /// <summary>
        /// Initializes the client
        /// </summary>
        public abstract void Init();
    }

}

