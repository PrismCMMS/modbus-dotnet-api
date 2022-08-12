using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Util;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.Repository;
using Com.DaacoWorks.Modbus.Pdu;
using Daacoworks.Extension.ResponseHandlers;
using System.Collections.Generic;

namespace DaacoWorks.Extension.ResponseHandlers
{
    public class GenericModbusResponseProcessor : IResponseProcessor
    {
        List<IResponseProcessor> specializedResponseProcessors;

        public GenericModbusResponseProcessor()
        {
            specializedResponseProcessors = new List<IResponseProcessor>();
        }

        public void AddResponseProcessor(IResponseProcessor specializedResponseProcessor)
        {
            specializedResponseProcessors.Add(specializedResponseProcessor);
        }

        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            var request = responseWrapper.Response.Request as ModbusRequest;
            if (request == null) return false;
            if (responseWrapper.DeviceData == null) return false;

            var deviceData = responseWrapper.DeviceData;
            
            if (responseWrapper.Response is ModbusSuccessResponse successResponse)
            {
                SetRequestInformation(request, deviceData);
                SetResponseInformation(successResponse, deviceData);
                RunSpecializedResponseProcessor(responseWrapper);
                DeviceResponseRepository.SaveData(deviceData);
                return true;
            }
            else if (responseWrapper.Response is ModbusErrorResponse errorResponse)
            {
                deviceData.ResponseErrorCode = errorResponse.GetErrorCode();
                deviceData.ResponseErrorMessage = errorResponse.GetErrorMessage();
                DeviceResponseRepository.SaveData(deviceData);
                return true;
            }
            else
            {
                return false;
            }   
        }

        private void RunSpecializedResponseProcessor(ResponseWrapper responseWrapper)
        {
            foreach (var processor in specializedResponseProcessors)
            {
                if (processor.ProcessResponse(responseWrapper))
                    return; //when we find first spl processor that handled the response we stop
            }
        }

        private void SetRequestInformation(ModbusRequest request, DeviceData deviceData)
        {
            var requestIdentifer = (CustomRequestIdentifier)request.RequestIdentifier;

            deviceData.RequestId = requestIdentifer.RequestId;
            deviceData.RequestTime = requestIdentifer.RequestTime;
            deviceData.IsScheduledRequest = request.IsScheduledRequest;
            deviceData.RequestSlaveId = request.SlaveId;
            deviceData.RequestFunctionCode = request.GetFunctionCode();
            deviceData.RequestReadAddress = request.StartAddress;
            deviceData.RequestReadQuantity = request.Quantity;
        }

        private void SetResponseInformation(ModbusSuccessResponse response, DeviceData deviceData)
        {
            deviceData.ResponseRawData = response.GetData();
            deviceData.ResponseFloatData = ModbusUtil.ToFloatValue(deviceData.ResponseRawData, false, true)[0];
            deviceData.ResponseIntData = ModbusUtil.ToIntValue(deviceData.ResponseRawData, false, true)[0];
        }
    }
}
