using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ReportModels
{
    public class QuestionAnalysisCollection
    {
        public int QuestionNumber { get; set; }
        public Dictionary<string, int> Summary { get; set; }

        public QuestionAnalysisCollection()
        {
            Summary = new Dictionary<string, int>();
        }
    }
}
