using ProjectWebAPI.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ResponseModels
{
    public class CSVResponse
    {
        public int SurveyID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public List<QuestionResponse> Responses { get; set; }

        public CSVResponse()
        {
            Responses = new List<QuestionResponse>();
        }
    }
}
