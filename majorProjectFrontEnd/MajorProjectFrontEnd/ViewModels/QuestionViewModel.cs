using MajorProjectFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.ViewModels
{
	public class QuestionViewModel
	{
		public int QuestionNumber { get; set; }
		public int SurveyID { get; set; }
		public string Question { get; set; }
		public string Type { get; set; }
		public string Options { get; set; }
		public List<SurveyDataModel> surveyDataModels { get; set; }
	}
}
