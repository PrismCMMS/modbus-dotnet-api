using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusTCPUDPDecoder is a decoder class invoked to decoder the ModbusTCP responses.
    /// </summary>
    public class ModbusTCPUDPDecoder : IDecoder<Protocol.Model.Response>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusTCPUDPDecoder).FullName);

        /// <summary>
        /// Decodes the TCP response received as input
        /// </summary>
        /// <param name="connectionParameters">connection parameter</param>
        /// <param name="input">the input</param>
        /// <returns></returns>
        public virtual Protocol.Model.Response Decode(ConnectionParameters connectionParameters, MemoryStream input)
        {
            
                ushort pduLength = 0;
                int requestId = GetRequestID(input);
                logger.Info("Client Decode modbus pdu requestId " + requestId);


                int protocolId = input.ReadShort();
                logger.Info("Client Decode modbus pdu protocol id " + protocolId);

                pduLength = input.ReadShort();
                pduLength -= 2;// reducing unitId and functionCode from pduLength
                logger.Info("Client Decode modbus pdu length " + pduLength);

                var unitId = input.ReadByte();
                logger.Info("Client Decode unit id " + unitId);

                //1 byte functionCode, data
                int functionCode = input.ReadByte() & 0x000000FF;
                logger.Info("Client Decode functionCode " + functionCode);

                return (new ModbusResponseParser()).ParseModbusResponsePDU(new ModbusRequestIdentifier(connectionParameters.GetHost(), functionCode, requestId), pduLength, functionCode, input);
            
        }

        private int GetRequestID(MemoryStream input)
        {
            byte[] dst = new byte[] { 0, 0 };
            input.Read(dst);
            int transactionID = ModbusUtil.ToInt32(dst, 0);
            logger.Info("Client Decode modbus pdu transaction id " + transactionID);

            return transactionID;

        }

    }
}