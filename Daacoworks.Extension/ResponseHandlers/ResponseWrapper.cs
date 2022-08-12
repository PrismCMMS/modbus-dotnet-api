using Com.DaacoWorks.Protocol.Model;
using DaacoWorks.Extension.Model;
using System;

namespace DaacoWorks.Extension.ResponseHandlers
{
    public class ResponseWrapper
    {             

        public ResponseWrapper(Response response, DateTime responseTime)
        {
            this.Response = response;
            this.DeviceData = new DeviceData() { ResponseTime = responseTime };
        }

        public Response Response { get; private set; }
        public DeviceData DeviceData { get; private set; }
    }
}
