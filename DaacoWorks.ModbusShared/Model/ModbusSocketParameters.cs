using Com.DaacoWorks.Protocol.Model;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Model
{
    /// <summary>
    /// ModbusSocketParameters class represents the parameters required for establishing a socket connection with
    /// Modbus server.
    /// </summary>
    public class ModbusSocketParameters : SocketParameters
    {
        private ModbusType type;

        /// <summary>
        /// Instantiates a new modbus socket parameters.
        /// </summary>
        /// <param name="hostName">the host name</param>
        /// <param name="port">port number</param>
        /// <param name="type">modbus type</param>
        public ModbusSocketParameters(string hostName, int port, ModbusType type):base(hostName, port, GetConnectionType(type))
        {            
            this.type = type;
        }        

        private static ConnectionType GetConnectionType(ModbusType type)
        {
            switch (type)
            {
                case ModbusType.TCP:
                case ModbusType.RTU_OVER_TCP:
                case ModbusType.ASCII_OVER_TCP:
                    return ConnectionType.TCP;
                case ModbusType.UDP:
                case ModbusType.RTU_OVER_UDP:
                case ModbusType.ASCII_OVER_UDP:
                    return ConnectionType.UDP;
            }
            return ConnectionType.TCP;
        }

        /// <summary>
        /// Gets the modbus type
        /// </summary>
        /// <returns></returns>
        public ModbusType GetModbusType()
        {
            return type;
        }
        

    }
}