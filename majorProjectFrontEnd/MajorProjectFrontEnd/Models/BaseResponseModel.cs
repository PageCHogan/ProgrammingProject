using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.Models
{
	public class BaseResponseModel
	{
		public int ResponseID { get; set; }
		public int UserID { get; set; }
		public int SurveyID { get; set; }
		public string ResponseCSV { get; set; }
		public DateTime Date { get; set; }

		public BaseResponseModel()
		{
			ResponseCSV = "";
			Date = DateTime.MinValue;
		}
	}
}