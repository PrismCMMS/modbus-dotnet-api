using Com.DaacoWorks.Modbus.Pdu.Executor;
using Com.DaacoWorks.Modbus.Pdu.Util;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{
    /// <summary>
    /// ModbusASCIIOverTCPUDPEncoder is a encoder class invoked to encode the ModbusASCII requests.
    /// </summary>
    public class ModbusASCIIOverTCPUDPEncoder : ModbusTCPUDPEncoder
    {

        /// <summary>
        /// Encode the request object into output buffer.
        /// </summary>
        /// <param name="metaInfo">meta information</param>
        /// <param name="output">output as memorystream</param>
        public override void Encode(ModbusPDUWrapper metaInfo, MemoryStream output)
        {
            base.EncodeHeader(metaInfo, output);
            byte[] data = metaInfo.Pdu.GetDataInBytes();
            data = ModbusUtil.GetASCII(data);
            output.Write(data, (int)output.Position, data.Length);
            data = ModbusUtil.LRC(data);
            output.Write(data, (int)output.Position, data.Length);
        }

    }
}