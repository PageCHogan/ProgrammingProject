using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Messages
{
    public class BaseResponse
    {
        public string Status { get; set; }

        public BaseResponse()
        {
            Status = "Failed";
        }
    }
}
