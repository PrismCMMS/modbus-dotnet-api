using Com.DaacoWorks.Modbus.Codec;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModBusTest.Codec
{
    [TestClass]
    public class ModbusTCPUDPDecoderTest
    {
        [TestMethod]
        public void DecodeTest()
        {
            //var data = new byte[] { 0x01, 0x03, 0x0, 0x0, 0x0, 0x03, 0x0, 0x01, 0x03, 0x0A };
            //var stream = new MemoryStream(data);
            //var decoder = new ModbusTCPUDPDecoder();
            //var connectionParam = new SocketParameters("host", 1601, ConnectionType.TCP);

            //var request = new ReadCoilsRequest(1, 1, 1, true);
            //var map = RequestMap<ReadCoilsRequest, ReadCoilsResponse, ErrorResponse>.GetInstance();

            //    map.AddRequestPDUMetaInfo(new ModbusPDUWrapper<ReadCoilsRequest, ReadCoilsResponse, ErrorResponse>(null, request));
            //var response = decoder.Decode(connectionParam, stream) as ReadCoilsResponse;
            //Assert.IsNotNull(response);
        }
    }
}
