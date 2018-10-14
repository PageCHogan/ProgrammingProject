using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ReportModels
{
    public class ReportDataModel
    {
        public int ReportID { get; set; }
        public int ResponseID { get; set; }
        public string Name { get; set; }
        public string ReportFile { get; set; }
        public DateTime Date { get; set; }

        public ReportDataModel()
        {
            //ReportID = 0;
            //ResponseID = 0;
            Name = "";
            ReportFile = "";
            Date = DateTime.MinValue;
        }
    }
}
