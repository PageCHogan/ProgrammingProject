using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.Models
{
	public class UserDataModel
	{
		public int UserID { get; set; }
		public string Username { get; set; }
		public string Title { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Type { get; set; }
		public string Permission { get; set; }
		public string Groups { get; set; }


		public UserDataModel()
		{
			Username = "";
			Title = "";
			Firstname = "";
			Lastname = "";
			Email = "";
			Password = "";
			Type = "";
			Permission = "";
			Groups = "";
		}
	}
}
