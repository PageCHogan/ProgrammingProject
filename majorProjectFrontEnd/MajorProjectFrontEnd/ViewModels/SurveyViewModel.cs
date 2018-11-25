using MajorProjectFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.ViewModels
{
	public class SurveyViewModel
	{
		public int SurveyID { get; set; }
		public string SurveyName { get; set; }
		public int UserID { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Permission { get; set; }

		public List<UserDataModel> userDataModels { get; set; }
	}
}
