using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ReportModels
{
    public class ReportAnalysisModel
    {
        public string ReportTitle { get; set; }
        public string SurveyTitle { get; set; }
        public DateTime ReportDate { get; set; }
        public List<ReportResponseAnalysis> Responses { get; set; }

        public ReportAnalysisModel()
        {
            ReportTitle = "";
            SurveyTitle = "";
            ReportDate = DateTime.Now;
            Responses = new List<ReportResponseAnalysis>();
        }
    }
}
