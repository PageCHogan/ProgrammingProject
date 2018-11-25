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
        public List<SurveyDataModel> GetSurveys(int? surveyID = null)
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string SqlQuery = "SELECT surveyID, survey_name, userID, type, description, start_date, end_date, permission FROM Survey";

            try
            {
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return surveyData;
        }

        public bool AddNewSurvey(SurveyDataModel survey)
        {
            bool result = false;

            if (survey != null)
            {
                string SqlQuery = "INSERT INTO Survey (survey_name, userID, type, description, start_date, end_date, permission) VALUES (@survey_name, @userID, @type, @description, @start_date, @end_date, @permission)";

                try
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = CONNECTION_STRING;
                        conn.Open();

                        SqlCommand command = new SqlCommand(SqlQuery, conn);

                        if (SqlQuery.Length > 0)
                        {
                            command = new SqlCommand(SqlQuery, conn);
                            command.Parameters.AddWithValue("@survey_name", survey.SurveyName = survey.SurveyName ?? "");
                            command.Parameters.AddWithValue("@userID", survey.UserID);
                            command.Parameters.AddWithValue("@type", survey.Type = survey.Type ?? "");
                            command.Parameters.AddWithValue("@description", survey.Description = survey.Description ?? "");
                            command.Parameters.AddWithValue("@start_date", survey.StartDate);
                            command.Parameters.AddWithValue("@end_date", survey.EndDate);
                            command.Parameters.AddWithValue("@permission", survey.Permission = survey.Permission ?? "");
                        }
                        int sqlResult = command.ExecuteNonQuery();

                        result = sqlResult < 0 ? false : true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                }
            }

            if (!result)
                System.Diagnostics.Debug.WriteLine("Error - record not saved to database");

            return result;
        }

        public bool UpdateSurvey(SurveyDataModel survey)
        {
            bool result = false;

            if (survey != null)
            {
                string SqlQuery = "UPDATE Survey SET survey_name = @survey_name, userID = @userID, type = @type, description = @description, start_date = @start_date, end_date = @end_date, permission = @permission WHERE SurveyID = " + survey.SurveyID;

                try
                {
                    using (SqlConnection conn = new SqlConnection())
                    {
                        conn.ConnectionString = CONNECTION_STRING;
                        conn.Open();

                        SqlCommand command = new SqlCommand(SqlQuery, conn);

                        if (SqlQuery.Length > 0)
                        {
                            command = new SqlCommand(SqlQuery, conn);
                            command.Parameters.AddWithValue("@survey_name", survey.SurveyName = survey.SurveyName ?? "");
                            command.Parameters.AddWithValue("@userID", survey.UserID);
                            command.Parameters.AddWithValue("@type", survey.Type = survey.Type ?? "");
                            command.Parameters.AddWithValue("@description", survey.Description = survey.Description ?? "");
                            command.Parameters.AddWithValue("@start_date", survey.StartDate);
                            command.Parameters.AddWithValue("@end_date", survey.EndDate);
                            command.Parameters.AddWithValue("@permission", survey.Permission = survey.Permission ?? "");
                        }
                        int sqlResult = command.ExecuteNonQuery();

                        result = sqlResult < 0 ? false : true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                }
            }

            if (!result)
                System.Diagnostics.Debug.WriteLine("Error - record not updated in database");

            return result;
        }

        public bool DeleteSurvey(int ID)
        {
            bool result = false;

            string SqlQuery = "DELETE FROM Survey WHERE SurveyID = @surveyID";

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = CONNECTION_STRING;
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlQuery, conn);

                    if (SqlQuery.Length > 0)
                    {
                        command = new SqlCommand(SqlQuery, conn);
                        command.Parameters.AddWithValue("@surveyID", ID);
                    }

                    int sqlResult = command.ExecuteNonQuery();

                    result = sqlResult < 0 ? false : true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            if (!result)
                System.Diagnostics.Debug.WriteLine("Error - record not deleted");

            return result;
        }

    }
}
