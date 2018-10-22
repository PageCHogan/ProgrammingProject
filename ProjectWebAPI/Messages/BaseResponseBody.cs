using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProjectWebAPI.Messages
{
    public class BaseResponseBody
    {
        public string Result { get; set; }

        public BaseResponseBody()
        {
            Result = "";
        }
    }
}
