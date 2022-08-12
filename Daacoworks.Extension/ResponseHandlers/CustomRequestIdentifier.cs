using Com.DaacoWorks.Protocol.Model;
using System;

namespace DaacoWorks.Extension.ResponseHandlers
{
    public class CustomRequestIdentifier: RequestIdentifier
    {

        private DateTime requestTime;
        private Guid requestId;

        public CustomRequestIdentifier()
        {
            
            this.requestTime = DateTime.Now;
            requestId = Guid.NewGuid();
        }

        public Guid RequestId { get { return requestId; } }
        public DateTime RequestTime { get { return requestTime; } }
    }
}
