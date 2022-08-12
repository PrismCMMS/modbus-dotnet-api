using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class MaskWriteRegisterProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as MaskWriteRegisterRequest, responseWrapper.DeviceData);
                SetResponseInformation(responseWrapper.Response as MaskWriteRegisterResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is MaskWriteRegisterResponse;
        }

        private void SetRequestInformation(MaskWriteRegisterRequest request, DeviceData deviceData)
        {            
            deviceData.RequestAndMask = request.AndMask;
            deviceData.RequestOrMask = request.OrMask;
        }

        private void SetResponseInformation(MaskWriteRegisterResponse response, DeviceData deviceData)
        {
            deviceData.ResponseAndMask = response.GetAndMask();
            deviceData.ResponseOrMask = response.GetOrMask();
        }
    }
}
