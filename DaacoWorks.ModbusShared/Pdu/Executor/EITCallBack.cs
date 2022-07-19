using Com.DaacoWorks.Modbus.Model;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Extensions;
using Com.DaacoWorks.Protocol.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Com.DaacoWorks.Modbus.Pdu.Executor
{
    /// <summary>
    /// EITCallBack class is a callback for notifying the response for <see cref="ReadDeviceIdentificationRequest"/>
    /// </summary>
    public class EITCallBack : IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse>
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(EITCallBack).FullName);

        private IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack;
        private ReadDeviceIdentificationRequest pdu;
        private ReadDeviceIdentificationResponse deviceIdResp;
        private int counter = 1;
        private IConnection connection;

        /// <summary>
        /// Instantiates a new EIT call back.
        /// </summary>
        /// <param name="pdu">the pdu</param>
        /// <param name="callBack">the call back</param>
        /// <param name="connection">the connection</param>
        public EITCallBack(ReadDeviceIdentificationRequest pdu, IResponseCallback<ModbusSuccessResponse, ModbusErrorResponse> callBack, IConnection connection)
        {
            this.callBack = callBack;
            this.pdu = pdu;
            this.connection = connection;
            this.deviceIdResp = new ReadDeviceIdentificationResponse(pdu);
        }

        /// <summary>
        /// OnSuccess receives the response data for the request from peer
        /// </summary>
        /// <param name="response"></param>
        public void OnSuccess(ModbusSuccessResponse response)
        {
            MemoryStream buffer = new MemoryStream(response.GetData());
            buffer.ReadByte();//ignore MEI type
            buffer.ReadByte();//ignore deviceId code
            buffer.ReadByte();//ignore conformity level
            if (IsResponsePending(buffer))
            {
                logger.Debug("ReadDeviceIdentification submitted " + (++counter));
                pdu.ObjectId = GetNextObjectId(buffer);
                deviceIdResp.AddObjects(GetObjects(buffer));
                try
                {
                    ExecutorFactory.GetGlobalRequestExecutor().Submit(new ModbusRunnableTask(pdu, this, connection, CancellationToken.None));
                }
                catch (Exception e)
                {
                    logger.Error("ReadDeviceIdentification response received " + counter, e);
                }
            }
            else
            {
                logger.Debug("ReadDeviceIdentification response received " + counter);
                counter = 0;
                GetNextObjectId(buffer);//ignore next object id
                deviceIdResp.AddObjects(GetObjects(buffer));
                callBack.OnSuccess(deviceIdResp);
            }
        }

        private DeviceInfo[] GetObjects(MemoryStream buffer)
        {
            DeviceInfo[] deviceInfos = new DeviceInfo[buffer.ReadByte()]; //number of objects
            var index = 0;
            while (buffer.HasRemaining())
            {
                byte objectId = (byte)buffer.ReadByte();
                byte objectLength = (byte)buffer.ReadByte();
                byte[] objectValue = new byte[objectLength];
                buffer.Read(objectValue, 0, objectLength);
                DeviceInfo devInfo = new DeviceInfo(objectId, objectValue);
                deviceInfos[index++]= devInfo;
            }

            return deviceInfos;
        }

        private byte GetNextObjectId(MemoryStream buffer)
        {
            return (byte)buffer.ReadByte();
        }

        private bool IsResponsePending(MemoryStream buffer)
        {
            byte respPending = (byte)buffer.ReadByte();
            return respPending == (byte)0xFF;
        }

        /// <summary>
        /// OnError received the error data received from peer
        /// </summary>
        /// <param name="error"></param>
        public void OnError(ModbusErrorResponse error)
        {
            callBack.OnError(error);
        }



    }
}