using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using Com.DaacoWorks.Protocol.Util;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusRTUOverTCPUDPDecoder is a decoder class invoked to decode the ModbusRTU responses.
    /// </summary>
    public class ModbusRTUOverTCPUDPDecoder : IDecoder<Protocol.Model.Response>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusRTUOverTCPUDPDecoder).FullName);

        /// <summary>
        /// Decodes the RTU response
        /// </summary>
        /// <param name="connectionParameters">connection parameters</param>
        /// <param name="input">the input</param>
        /// <returns></returns>
        public Protocol.Model.Response Decode(ConnectionParameters connectionParameters, MemoryStream input)
        {
            using (var buffer = new MemoryStream())
            {
                input.CopyTo(buffer);
                if (PerformCRC(input))
                {
                    int slaveId = input.ReadByte();
                    int functionCode = input.ReadByte();
                    input.Capacity = input.Capacity - 2;
                    int pduLength = input.Capacity - 2;
                    return (new ModbusResponseParser()).ParseModbusResponsePDU(new ModbusRequestIdentifier(connectionParameters.GetHost(), functionCode, slaveId), pduLength, functionCode,input);
                }

                else
                {
                    logger.Warn("Incorrect Modbus Response. CRC check failed. data " + ProtocolUtils.GetHexByteString(input));
                    return null;
                }
            }
        }

        bool PerformCRC(MemoryStream buffer)
        {
            byte[] data = new byte[(buffer.Length - 2)];
            buffer.Read(data);
            byte[] sourceCRC = new byte[2];
            buffer.Read(sourceCRC, (int)buffer.Position, 2);
            byte[] computedCRC = ModbusUtil.CRC16(data);
            buffer.Flip();
            return computedCRC[0] == sourceCRC[1] && computedCRC[1] == sourceCRC[0];
        }

    }
}