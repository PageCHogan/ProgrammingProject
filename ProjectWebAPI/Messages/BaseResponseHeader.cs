using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectWebAPI.Messages
{
    public class BaseResponseHeader
    {
        public HttpStatusCode Code { set; get; }
        public bool Success { set; get; }
        public string Message { set; get; }

        public BaseResponseHeader(HttpStatusCode code, bool success, string message)
        {
            Code = code;
            Success = success;
            Message = message;
        }

        public BaseResponseHeader()
        {
            Code = HttpStatusCode.NotImplemented;
            Success = true;
            Message = "";
        }
    }
}
