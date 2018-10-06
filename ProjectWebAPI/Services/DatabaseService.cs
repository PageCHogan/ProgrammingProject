using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Services
{
	public class DatabaseService
	{
		private const string CONNECTION_STRING = "Data Source=sebens.database.windows.net;Initial Catalog=Seben;Persist Security Info=True;User ID=sebens;Password=EveryoneHD!";

		public DatabaseService()
		{
			//Empty Constructor...for now
		}

		public List<StaffTestDataModel> GetTestData(int? userID = null)
		{
			List<StaffTestDataModel> testData = new List<StaffTestDataModel>();
			string SqlQuery = "SELECT * FROM Staff";

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

				if (userID.HasValue)
				{
					//SqlQuery += " Where ID = @0";
					command = new SqlCommand(SqlQuery, conn);
					//command.Parameters.Add(new SqlParameter("0", userID));
				}
				else
				{
					command = new SqlCommand(SqlQuery, conn);
				}

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						//if (Int32.TryParse(reader[0].ToString(), out int id))
						//{
						testData.Add(new StaffTestDataModel()
						{
							StaffID = reader[0].ToString(),
							Name = reader[1].ToString(),
							Password = reader[2].ToString(),
							Type = reader[3].ToString(),
							Groups = reader[4].ToString()
						});
						//}
					}
				}
			}

			return testData;
		}

		public List<ResponseDataModel> GetResponseData()
		{
			List<ResponseDataModel> data = new List<ResponseDataModel>();

			string sqlquery = "SELECT Response.ResponseID, Survey.survey_name, Survey.type, Survey.description, Survey.date, Staff.name, Response.responseCSV, Response.date\n" +
                "FROM (	(Response INNER JOIN Survey ON Response.surveyID = Survey.surveyID) INNER JOIN\n" +
                "Staff ON Survey.staffID = Staff.staffID\n" +
                ");";
            
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

				command = new SqlCommand(sqlquery, conn);

				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						data.Add(new ResponseDataModel()
						{
							ResponseID = reader[0].ToString(),
							SurveyName = reader[1].ToString(),
							SurveyType = reader[2].ToString(),
							SurveyDescription = reader[3].ToString(),
							SurveyDate = reader[4].ToString(),
							StaffName = reader[5].ToString(),
							ResponseCSV = reader[6].ToString(),
							ResponseDate = reader[7].ToString()
						});
					}
				}
			}

			return data;
		}
	}
}
