using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Models.ViewModels
{
    public class ResponseDataViewModel
    {
        public List<ResponseDataModel> Responses { get; set; }

        public ResponseDataViewModel()
        {
            Responses = new List<ResponseDataModel>();
        }
    }
}
