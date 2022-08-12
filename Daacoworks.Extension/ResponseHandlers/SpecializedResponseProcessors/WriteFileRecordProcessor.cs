using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Util;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class WriteFileRecordProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as WriteFileRecordRequest, responseWrapper.DeviceData);
                SetResponseInformation(responseWrapper.Response as WriteFileRecordResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is WriteFileRecordResponse;
        }

        private void SetRequestInformation(WriteFileRecordRequest request, DeviceData deviceData)
        {
            deviceData.RequestFileRecords = request.FileRecords;
        }

        private void SetResponseInformation(WriteFileRecordResponse response, DeviceData deviceData)
        {
            deviceData.ResponseFileRecords = response.GetFileRecords();
        }
    }
}
