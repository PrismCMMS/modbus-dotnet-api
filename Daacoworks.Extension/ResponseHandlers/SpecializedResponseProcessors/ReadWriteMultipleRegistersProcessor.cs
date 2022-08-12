using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.DaacoWorks.Modbus.Pdu.Request;
using Com.DaacoWorks.Modbus.Pdu.Response;
using Com.DaacoWorks.Modbus.Pdu.Util;
using DaacoWorks.Extension.Model;
using DaacoWorks.Extension.ResponseHandlers;

namespace Daacoworks.Extension.ResponseHandlers.SpecializedResponseProcessors
{
    public class ReadWriteMultipleRegistersProcessor : IResponseProcessor
    {
        public bool ProcessResponse(ResponseWrapper responseWrapper)
        {
            if (CanProcessResponse(responseWrapper))
            {
                SetRequestInformation(responseWrapper.Response.Request as ReadWriteMultipleRegistersRequest, responseWrapper.DeviceData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanProcessResponse(ResponseWrapper responseWrapper)
        {
            return responseWrapper.Response is ReadWriteMultipleRegistersResponse;
        }

        private void SetRequestInformation(ReadWriteMultipleRegistersRequest request, DeviceData deviceData)
        {
            deviceData.RequestWriteAddress = request.WriteAddress;
            deviceData.RequestWriteQuantity = request.WriteQuantity;
            //deviceData.RequestWriteValues = request.WriteValues;
        }
        

    }
}
