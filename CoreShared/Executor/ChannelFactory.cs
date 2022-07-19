using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    ///  Factory class to create instance of channel interface.
    /// </summary>
    class ChannelFactory
    {

        public static IChannel GetChannel(ConnectionParameters connectionParams)
        {
            IChannel channel = null;
            switch (connectionParams.GetConnectionType())
            {
                case ConnectionType.TCP:
                    channel = new TCPChannel(connectionParams);
                    break;
                case ConnectionType.UDP:
                    channel = new UDPChannel(connectionParams);
                    break;
                default:
                    break;
            }
            return channel;
        }

    }
}