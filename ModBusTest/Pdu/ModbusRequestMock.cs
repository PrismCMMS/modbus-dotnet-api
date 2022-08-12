using Com.DaacoWorks.Modbus.Pdu;

namespace ModBusTest.Pdu
{
    public class ModbusRequestMock : ModbusRequest
    {
        public ModbusRequestMock(byte slaveId, ushort startAddress, ushort quantity, bool convertToHex) : base(slaveId, startAddress, quantity, convertToHex)
        {
        }

        public override byte GetFunctionCode()
        {
            return 0x01;
        }

        public override void Validate()
        {
            
        }

    }
}
