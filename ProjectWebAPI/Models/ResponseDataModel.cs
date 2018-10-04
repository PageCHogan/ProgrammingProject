using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models
{
    public class ResponseDataModel
    {
        public string ResponseID { get; set; }
        public string SurveyName { get; set; }
        public string SurveyType { get; set; }
        public string SurveyDescription { get; set; }
        public string SurveyDate { get; set; }
        public string StaffName { get; set; }
        public string ResponseCSV { get; set; }
        public string ResponseDate { get; set; }

        public ResponseDataModel()
        {
            ResponseID = "";
            SurveyName = "";
            SurveyType = "";
            SurveyDescription = "";
            SurveyDate = "";
            StaffName = "";
            ResponseCSV = "";
            ResponseDate = "";

        }
    }


}
