using Com.DaacoWorks.Modbus.Pdu;
using Com.DaacoWorks.Protocol.Executor;

namespace Com.DaacoWorks.Modbus.Response.Callback
{
    /// <summary>
    /// ModbusResponseCallback interface represents methods to handle the responses.
    /// </summary>
    public interface IModbusResponseCallback : IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse>
    {


    }
}