using Com.DaacoWorks.Modbus.Header;
using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Executor;
using Com.DaacoWorks.Protocol.Codec;
using Com.DaacoWorks.Protocol.Logger;
using System.IO;

namespace Com.DaacoWorks.Modbus.Codec
{

    /// <summary>
    /// ModbusTCPUDPEncoder is a encoder class invoked to encode the ModbusTCP requests.
    /// </summary>
    public class ModbusTCPUDPEncoder : IEncoder<ModbusPDUWrapper>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusTCPUDPEncoder).FullName);

        /// <summary>
        /// Encodes the TCP request
        /// </summary>
        /// <param name="metaInfo">meta information</param>
        /// <param name="output">the output</param>
        public virtual void Encode(ModbusPDUWrapper metaInfo, MemoryStream output)
        {

            logger.Info("Inside TCP/UDP Encoder requestId " + metaInfo.GetRequestId());

            EncodeHeader(metaInfo, output);
            //write pdu
            var data = metaInfo.Pdu.GetDataInBytes();
            //output.Write(data, (int)output.Position, data.Length);
            output.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Encode header.
        /// </summary>
        /// <param name="metaInfo">meta information</param>
        /// <param name="output">the output</param>
        protected void EncodeHeader(ModbusPDUWrapper metaInfo, MemoryStream output)
        {
            Protocol.Headers.Header header = new ModbusTCPHeader(metaInfo.Pdu);
            ((ModbusTCPHeader)header).SetRequestId(((ModbusRequestIdentifier)metaInfo.GetRequestId()).GetRequestId());
            //write header
            var data = header.GetHeaderInBytes();
            //output.Write(data, (int)output.Position, data.Length);
            output.Write(data, 0, data.Length);
        }
    }
}