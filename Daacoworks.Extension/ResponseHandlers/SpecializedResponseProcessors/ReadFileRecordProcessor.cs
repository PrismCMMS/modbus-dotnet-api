using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class ReadFileRecordProcessor : IResponseProcessor
    {

        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as ReadFileRecordRequest, responseWrapper.DeviceData);
                SetResponseInformation(responseWrapper.Response as ReadFileRecordResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is ReadFileRecordResponse;
        }

        private void SetRequestInformation(ReadFileRecordRequest request, DeviceData deviceData)
        {
            deviceData.RequestFileRecords = request.GetFileRecords();
        }

        private void SetResponseInformation(ReadFileRecordResponse response, DeviceData deviceData)
        {
            deviceData.ResponseFileRecords = response.GetFileRecords();
        }
    }
}
