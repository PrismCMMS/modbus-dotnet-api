using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Clients;

namespace Com.DaacoWorks.Modbus.Clients {

    /// <summary>
    /// IModbusClient interface defines the basic operations to be supported for submitting request to modbus supported devices.
    /// </summary>
    public interface IModbusClient : IClient<ModbusRequest, Protocol.Model.Response, ModbusSuccessResponse, ModbusErrorResponse> {

    }
}