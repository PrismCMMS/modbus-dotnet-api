using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.Threading;
using static Com.DaacoWorks.Modbus.Pdu.Constants.Constants;

namespace Com.DaacoWorks.Modbus.Pdu.Executor
{
    /// <summary>
    /// ModbusRunnableTask is an abstract class which represents Modbus Task to be submitted in the Request Executor.
    /// </summary>
    public class ModbusRunnableTask : ExecutorTask<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ModbusRunnableTask).FullName);

        /// <summary>
        /// Instantiates a new modbus runnable task.
        /// </summary>
        /// <param name="pdu">the pdu</param>
        /// <param name="callBack">call back</param>
        /// <param name="connection">the connection</param>
        /// <param name="token">cancellation token</param>
        public ModbusRunnableTask(ModbusRequest pdu, IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack, IConnection connection, CancellationToken token) :
                base(pdu, callBack, connection, token)
        {

        }

        /// <summary>
        /// Submits the request to the socket
        /// </summary>
        protected override void InnerRun()
        {
            var modbusConnection = (ModbusConnection)connection;
            if (pdu.RequestIdentifier == null)
            {
                pdu.RequestIdentifier = modbusConnection.GetNextRequestId(pdu.GetFunctionCode(), pdu.SlaveId);
            }
            logger.Info("Inside Runnable Task request id " + pdu.RequestIdentifier);

            var pduWrapper = new ModbusPDUWrapper(callBack, pdu);

            //add it in the requestMap
            var requestMap = RequestMap<ModbusRequest, ModbusSuccessResponse, ModbusErrorResponse>.GetInstance();
                requestMap.AddRequestPDUMetaInfo(pduWrapper);

            try
            {
                if (CancellationToken.CanBeCanceled && CancellationToken.IsCancellationRequested) return;
                //write into socket
                modbusConnection.Write(pduWrapper);
            }
            catch (Exception e)
            {
                logger.Error("ModbusRunnableTask : Exception while writing the request on socket : ", e);
                ModbusErrorResponse error = new ModbusErrorResponse();
                error.Request = pdu;
                ModbusResponse respPdu = new ModbusResponse(pdu.GetFunctionCode(), new byte[] { 0x0B });
                error.SetResponsePDU(respPdu);
                requestMap.RemoveRequestPDUMetaInfo(pdu.RequestIdentifier);
                callBack.OnError(error);
            }

        }


    }
}