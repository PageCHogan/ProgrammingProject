using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.Models
{
	public class SurveyDataModel
	{
		public int SurveyID { get; set; }
		public string SurveyName { get; set; }
		public int UserID { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Permission { get; set; }

		public SurveyDataModel()
		{
			//SurveyID = 0;
			SurveyName = "";
			//UserID = 0;
			Type = "";
			Description = "";
			StartDate = DateTime.MinValue;
			EndDate = DateTime.MinValue;
			Permission = "";
		}

		override
		public string ToString()
		{
			return "SurveyID: " + SurveyID.ToString() + ", " + "SurveyName: " + SurveyName + ", " + "UserID: " + UserID.ToString() + ", "
			+ "Type: " + Type + ", " + "Description: " + Description + ", " + "StartDate: " + StartDate.ToString() + ", " +
			"EndDate: " + EndDate.ToString() + ", " + "Permission: " + Permission;
		}
	}
}
