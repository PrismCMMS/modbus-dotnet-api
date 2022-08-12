
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Util;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class WriteMultipleRegistersProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as WriteMultipleRegistersRequest, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is WriteMultipleRegistersResponse;
        }

        private void SetRequestInformation(WriteMultipleRegistersRequest request, DeviceData deviceData)
        {
            deviceData.RequestWriteValues = request.WriteValues;
        }
        
    }
}
