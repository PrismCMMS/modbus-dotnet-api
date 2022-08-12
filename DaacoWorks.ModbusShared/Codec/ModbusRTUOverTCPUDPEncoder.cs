using Com.DaacoWorks.Modbus.Pdu.Executor;
using Com.DaacoWorks.Modbus.Pdu.Util;
using Com.DaacoWorks.Protocol.Logger;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusRTUOverTCPUDPEncoder is a encoder class invoked to encode the ModbusRTU requests.
    /// </summary>
    public class ModbusRTUOverTCPUDPEncoder : ModbusTCPUDPEncoder
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusRTUOverTCPUDPEncoder).FullName);

        /// <summary>
        /// Encodes the RTU request
        /// </summary>
        /// <param name="metaInfo"></param>
        /// <param name="output"></param>
        public override void Encode(ModbusPDUWrapper metaInfo, MemoryStream output)
        {
            byte[] data = metaInfo.Pdu.GetDataInBytes();
            var buffer = new MemoryStream(1 + data.Length);//slaveId + pdu
            buffer.WriteByte(metaInfo.Pdu.SlaveId);
            buffer.Write(data, 0, data.Length);
            data = buffer.ToArray();

            output.Write(data, 0, data.Length);
            byte[] crc = ModbusUtil.CRC16(data);
            output.WriteByte(crc[1]);
            output.WriteByte(crc[0]);
        }
    }
}