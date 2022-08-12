using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class WriteSingleCoilProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetResponseInformation(responseWrapper.Response as WriteSingleCoilResponse, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is WriteSingleCoilResponse;
        }

        private void SetRequestInformation(WriteSingleCoilRequest request, DeviceData deviceData)
        {
            //deviceData.RequestCoilState = request.GetCoilState();
        }

        private void SetResponseInformation(WriteSingleCoilResponse response, DeviceData deviceData)
        {
            deviceData.ResponseCoilState = response.GetCoilState();
        }
    }
}
