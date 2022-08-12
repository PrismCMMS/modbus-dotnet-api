using Com.DaacoWorks.Modbus.Pdu.Executor;
using Com.DaacoWorks.Protocol.Codec;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// Factory class to provide encoder/decoder instances for a given modbus type.
    /// </summary>
    public class ModbusCodecFactory
    {
        /// <summary>
        /// Gets the encoder.
        /// </summary>
        /// <param name="type">modbus type</param>
        /// <returns>the encoder</returns>
        public static IEncoder<ModbusPDUWrapper> GetEncoder(ModbusType type)
        {
            IEncoder<ModbusPDUWrapper> encoder = null;
            switch (type)
            {
                case ModbusType.TCP:
                case ModbusType.UDP:
                    encoder = new ModbusTCPUDPEncoder();
                    break;
                case ModbusType.RTU_OVER_TCP:
                case ModbusType.RTU_OVER_UDP:
                    encoder = new ModbusRTUOverTCPUDPEncoder();
                    break;
                case ModbusType.ASCII_OVER_TCP:
                case ModbusType.ASCII_OVER_UDP:
                    encoder = new ModbusASCIIOverTCPUDPEncoder();
                    break;
                default:
                    encoder = new ModbusTCPUDPEncoder();
                    break;

            }
            return encoder;
        }

        /// <summary>
        /// Gets the decoder.
        /// </summary>
        /// <param name="type">modbus type</param>
        /// <returns>the decoder</returns>
        public static IDecoder<Protocol.Model.Response> GetDecoder(ModbusType type)
        {
            IDecoder<Protocol.Model.Response> decoder = null;
            switch (type)
            {
                case ModbusType.TCP:
                case ModbusType.UDP:
                    decoder = new ModbusTCPUDPDecoder();
                    break;
                case ModbusType.RTU_OVER_TCP:
                case ModbusType.RTU_OVER_UDP:
                    decoder = new ModbusRTUOverTCPUDPDecoder();
                    break;
                case ModbusType.ASCII_OVER_TCP:
                case ModbusType.ASCII_OVER_UDP:
                    decoder = new ModbusASCIIOverTCPUDPDecoder();
                    break;
                default:
                    decoder = new ModbusTCPUDPDecoder();
                    break;

            }
            return decoder;
        }

    }
}