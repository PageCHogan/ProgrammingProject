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

		public List<StaffDataModel> GetStaffData(int? staffID = null)
		{
			List<StaffDataModel> staffData = new List<StaffDataModel>();
			string SqlQuery = "SELECT StaffID, Name, Type, Groups FROM Staff";

			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

                //TODO: Remove, only testing as an ID while they are strings.
                string testID = "m5";

                //If a parameter (staffID) is passed, return data for that record else return ALL data.
                if (staffID.HasValue)
				{
					SqlQuery += " Where StaffID = @0";
					command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", testID)); //staffID));
                }
				else
				{
					command = new SqlCommand(SqlQuery, conn);
				}

				using (SqlDataReader reader = command.ExecuteReader())
				{
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            staffData.Add(new StaffDataModel()
                            {
                                StaffID = reader[0].ToString(),
                                Name = reader[1].ToString(),
                                Type = reader[2].ToString(),
                                Groups = reader[3].ToString()
                                //,Password = reader[4].ToString() //not really needed, bad practice to throw passwords around.
                            });
                        }
                    }
				}
			}

			return staffData;
		}

		public List<ResponseDataModel> GetResponseData(int? responseID = null)
		{
			List<ResponseDataModel> responseData = new List<ResponseDataModel>();

            //string SqlQuery = "SELECT Response.ResponseID, Survey.survey_name, Survey.type, Survey.description, Survey.date, Staff.name, Response.responseCSV, Response.date\n" +
            //             "FROM (	(Response INNER JOIN Survey ON Response.surveyID = Survey.surveyID) INNER JOIN\n" +
            //             "Staff ON Survey.staffID = Staff.staffID\n" +
            //             ");";

            string SqlQuery = "SELECT Response.ResponseID, Survey.survey_name, Survey.type, Survey.description, Survey.date, Staff.name, Response.responseCSV, Response.date " +
                "FROM Survey " +
                "INNER JOIN Response on Response.SurveyID = Survey.surveyID " +
                "INNER JOIN Staff on Staff.staffID = Survey.staffID ";
            
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

                //TODO: Remove, only testing as an ID while they are strings.
                string testID = "r1";

                //If a parameter (responseID) is passed, return data for that record else return ALL data.
                if (responseID.HasValue)
                {
                    SqlQuery += " Where ResponseID = @0";
                    command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", testID)); //responseID));
                }
                else
                {
                    command = new SqlCommand(SqlQuery, conn);
                }

				using (SqlDataReader reader = command.ExecuteReader())
				{
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            responseData.Add(new ResponseDataModel()
                            {
                                ResponseID = reader[0].ToString(),
                                SurveyName = reader[1].ToString(),
                                SurveyType = reader[2].ToString(),
                                SurveyDescription = reader[3].ToString(),
                                SurveyDate = Convert.ToDateTime(reader[4]),
                                StaffName = reader[5].ToString(),
                                ResponseCSV = reader[6].ToString(),
                                ResponseDate = Convert.ToDateTime(reader[7])
                            });
                        }
                    }
				}
			}

			return responseData;
		}
	}
}
