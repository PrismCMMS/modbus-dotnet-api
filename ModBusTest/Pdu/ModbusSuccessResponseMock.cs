using Com.DaacoWorks.Modbus.Pdu;

namespace ModBusTest.Pdu
{
    public class ModbusSuccessResponseMock : ModbusSuccessResponse
    {
        public ModbusSuccessResponseMock(ModbusRequest requestPDU) : base(requestPDU)
        {
        }
        
        public ModbusResponse GetResponse()
        {
            return responsePDU;
        }
    }
}
