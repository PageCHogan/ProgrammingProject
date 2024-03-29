﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.Models
{
	public class ResponseModel
	{
		public int surveyID { get; set; }
		public int questionNumber { get; set; }
		public string question { get; set; }
		public string options { get; set; }
		public string response { get; set; }
		public string questionType { get; set; }
		
		// E.g. 20180901
		public string date { get; set; }
		

		//E.g. 19:04:41 
		public string time { get; set; }
	}
}
