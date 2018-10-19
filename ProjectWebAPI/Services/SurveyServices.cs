using ProjectWebAPI.Models.SurveyModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Services
{
    public class SurveyServices : DatabaseService
    {
        //Survey Database Actions
        public List<SurveyDataModel> GetSurveyData(int? surveyID = null)
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string SqlQuery = "SELECT surveyID, survey_name, userID, type, description, start_date, end_date, permission FROM Survey";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNECTION_STRING;
                conn.Open();

                SqlCommand command;

                //If a parameter (survey) is passed, return data for that record else return ALL data.
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
                            surveyData.Add(new SurveyDataModel()
                            {
                                SurveyID = Convert.ToInt32(reader[0]),
                                SurveyName = reader[1].ToString(),
                                UserID = Convert.ToInt32(reader[2]),
                                Type = reader[3].ToString(),
                                Description = reader[4].ToString(),
                                StartDate = Convert.ToDateTime(reader[5]),
                                EndDate = Convert.ToDateTime(reader[6]),
                                Permission = reader[7].ToString()
                            });
                        }
                    }
                }
            }

            return surveyData;
        }
    }
}
