using DaacoWorks.Extension.ResponseHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daacoworks.Extension.ResponseHandlers
{
    public interface IResponseProcessor
    {
        bool ProcessResponse(ResponseWrapper responseWrapper);
    }
}
