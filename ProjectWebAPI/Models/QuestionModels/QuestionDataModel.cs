using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.QuestionModels
{
    public class QuestionDataModel
    {
        public int QuestionNumber { get; set; }
        public int SurveyID { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public string Options { get; set; }

        public QuestionDataModel()
        {
            //QuestionNumber = 0;
            //SurveyID = 0;
            Question = "";
            Type = "";
            Options = "";
        }
    }
}
