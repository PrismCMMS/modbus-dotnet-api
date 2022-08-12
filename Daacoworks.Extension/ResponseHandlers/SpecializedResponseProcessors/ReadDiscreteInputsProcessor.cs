using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class ReadDiscreteInputsProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetResponseInformation(responseWrapper.Response as ReadDiscreteInputsResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is ReadDiscreteInputsResponse;
        }
        

        private void SetResponseInformation(ReadDiscreteInputsResponse response, DeviceData deviceData)
        {
            deviceData.ResponseDicreteInputStatuses = response.GetDiscreteInputStatus();
        }
    }
}
