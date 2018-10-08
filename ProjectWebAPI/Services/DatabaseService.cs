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

                //If a parameter (staffID) is passed, return data for that record else return ALL data.
                if (staffID.HasValue)
				{
					SqlQuery += " Where StaffID = @0";
					command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", staffID));
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
                                StaffID = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                Type = reader[2].ToString(),
                                Groups = reader[3].ToString()
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

            string SqlQuery = "SELECT Response.ResponseID, Survey.survey_name, Survey.type, Survey.description, Staff.name, Response.responseCSV, Response.date " +
                "FROM Survey " +
                "INNER JOIN Response on Response.SurveyID = Survey.surveyID " +
                "INNER JOIN Staff on Staff.staffID = Survey.staffID ";
            
			using (SqlConnection conn = new SqlConnection())
			{
				conn.ConnectionString = CONNECTION_STRING;
				conn.Open();

				SqlCommand command;

                //If a parameter (responseID) is passed, return data for that record else return ALL data.
                if (responseID.HasValue)
                {
                    SqlQuery += " Where ResponseID = @0";
                    command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", responseID));
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
                                ResponseID = Convert.ToInt32(reader[0]),
                                SurveyName = reader[1].ToString(),
                                SurveyType = reader[2].ToString(),
                                SurveyDescription = reader[3].ToString(),
                                StaffName = reader[4].ToString(),
                                ResponseCSV = reader[5].ToString(),
                                ResponseDate = Convert.ToDateTime(reader[6])
                            });
                        }
                    }
				}
			}

			return responseData;
		}
	}
}
