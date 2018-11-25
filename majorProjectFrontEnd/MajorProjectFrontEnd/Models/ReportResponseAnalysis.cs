using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.ReportModels
{
	public class ReportResponseAnalysis
	{
		public int QuestionNumber { get; set; }
		public string Type { get; set; }
		public string Question { get; set; }
		public List<string> Message { get; set; }
		public string Options { get; set; }

		public ReportResponseAnalysis()
		{
			QuestionNumber = 0;
			Type = "";
			Question = "";
			Message = new List<string>();
			Options = "";
		}
	}
}
