using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MajorProjectFrontEnd.Models;

namespace MajorProjectFrontEnd.Services
{
	public class DatabaseService
	{
		private const string CONNECTION_STRING = "Data Source=sebens.database.windows.net;Initial Catalog=Seben;Persist Security Info=True;User ID=sebens;Password=EveryoneHD!";

		public DatabaseService()
		{
			//Empty Constructor...for now
		}

		//Returns all questions in database OR if surveyID provided, will return all questions belonging to that survey.
		public List<QuestionDataModel> GetQuestionData(int? surveyID = null)
		{
			List<QuestionDataModel> questionData = new List<QuestionDataModel>();
			string SqlQuery = "SELECT questionNumber, surveyID, question, type, options FROM Questions";

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

				//If a parameter (staffID) is passed, return data for that record else return ALL data.
				if (surveyID.HasValue)
				{
					SqlQuery += " Where SurveyID = @0";
					command = new SqlCommand(SqlQuery, conn);
					command.Parameters.Add(new SqlParameter("0", surveyID));
				}
				else
				{
					command = new SqlCommand(SqlQuery, conn);
				}

				using (SqlDataReader reader = command.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							questionData.Add(new QuestionDataModel()
							{
								QuestionNumber = Convert.ToInt32(reader[0]),
								SurveyID = Convert.ToInt32(reader[1]),
								Question = reader[2].ToString(),
								Type = reader[3].ToString(),
								Options = reader[4].ToString()
							});
						}
					}
				}
			}

			return questionData;
		}
	}
}
