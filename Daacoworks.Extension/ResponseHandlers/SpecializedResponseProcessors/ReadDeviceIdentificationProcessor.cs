using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class ReadDeviceIdentificationProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as ReadDeviceIdentificationRequest, responseWrapper.DeviceData);
                SetResponseInformation(responseWrapper.Response as ReadDeviceIdentificationResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is ReadDeviceIdentificationResponse;
        }

        private void SetRequestInformation(ReadDeviceIdentificationRequest request, DeviceData deviceData)
        {
            deviceData.RequestDeviceIdentificationType = (byte)request.DeviceId;
            deviceData.RequestObjectId = request.ObjectId;
        }

        private void SetResponseInformation(ReadDeviceIdentificationResponse response, DeviceData deviceData)
        {
            deviceData.ResponseDeviceInformation = response.GetDeviceInformation();
        }
    }
}
