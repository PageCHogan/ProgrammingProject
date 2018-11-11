using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ResponseModels
{
    public class ResponseDataModel
    {
        public int ResponseID { get; set; }
        public string SurveyName { get; set; }
        public string SurveyType { get; set; }
        public string SurveyDescription { get; set; }
        //public DateTime SurveyDate { get; set; }
        public string StaffName { get; set; }
        public string ResponseCSV { get; set; }
        public DateTime ResponseDate { get; set; }

        public ResponseDataModel()
        {
            ResponseID = 0;
            SurveyName = "";
            SurveyType = "";
            SurveyDescription = "";
            //SurveyDate = DateTime.MinValue;
            StaffName = "";
            ResponseCSV = "";
            ResponseDate = DateTime.MinValue;
        }
    }
}
