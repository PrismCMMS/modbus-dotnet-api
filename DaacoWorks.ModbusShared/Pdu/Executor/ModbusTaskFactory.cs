using Com.DaacoWorks.Modbus.Pdu.Constants;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using System.Threading;

namespace Com.DaacoWorks.Modbus.Pdu.Executor {

    /// <summary>
    /// ModbusTaskFactory is a factory to create instance of ModbusRunnableTask
    /// </summary>
    public class ModbusTaskFactory : TaskFactory<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse> {

        /// <summary>
        /// Gets ModbusRunnableTask instance
        /// </summary>
        /// <param name="requestObject"></param>
        /// <param name="callBack"></param>
        /// <param name="connection"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ExecutorTask<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse> GetRunnableTask(ModbusRequest requestObject, IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack, IConnection connection, CancellationToken cancellationToken) {
            switch (requestObject.GetFunctionCode()) {
                case FunctionCodes.ENCAPSULATED_INTERFACE_TRANSPORT:
                    return new ModbusRunnableTask(requestObject, new EITCallBack((ReadDeviceIdentificationRequest)requestObject, callBack, connection), connection, cancellationToken);
                default:
                    return new ModbusRunnableTask(requestObject, callBack, (IConnection)connection, cancellationToken);
            }
        }

    }
}