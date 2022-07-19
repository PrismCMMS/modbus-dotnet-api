using Com.DaacoWorks.Modbus.Client.Exception;
using Com.DaacoWorks.Protocol.Clients;
using Com.DaacoWorks.Protocol.Exception;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Modbus.Clients
{

    /// <summary>
    /// Factory class to create ModbusClient instances that represents socket connection with the modbus supported devices.
    /// </summary>
    public class ModbusClientFactory : ClientFactory<ModbusClient, ModbusException>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusClientFactory).FullName);

        private static ModbusClientFactory clientFactory = new ModbusClientFactory();

        private ModbusClientFactory()
        {

        }

        /// <summary>
        /// Gets the single instance of ModbusClientFactory.
        /// </summary>
        /// <returns>the ModbusClientFactory</returns>
        public static ModbusClientFactory GetInstance()
        {
            return clientFactory;
        }

        /// <summary>
        /// Creates an instance of ModbusClient
        /// </summary>
        /// <param name="connectionParameters"></param>
        /// <returns>the ModbusClient</returns>
        public override ModbusClient Create(ConnectionParameters connectionParameters)
        {
            try
            {
                return AddClient(new ModbusClient(connectionParameters));

            }
            catch (ConnectionException e)
            {
                throw new ModbusException(e.GetErrorCode(), e.Message);
            }
        }
    }
}

