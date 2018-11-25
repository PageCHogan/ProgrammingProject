using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebAPI.Models.ResponseModels;

namespace ProjectWebAPI.Services
{
    public class ResponseService : DatabaseService
    {
        public List<BaseResponseModel> GetResponses(int? responseID = null)
        {
            List<BaseResponseModel> responseData = new List<BaseResponseModel>();

            string SqlQuery = "SELECT ResponseID, UserID, SurveyID, ResponseCSV, Date FROM Response";

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = CONNECTION_STRING;
                    conn.Open();

                    SqlCommand command;

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
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                responseData.Add(new BaseResponseModel()
                                {
                                    ResponseID = Convert.ToInt32(reader[0]),
                                    UserID = Convert.ToInt32(reader[1]),
                                    SurveyID = Convert.ToInt32(reader[2]),
                                    ResponseCSV = reader[3].ToString(),
                                    Date = Convert.ToDateTime(reader[4])
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

            return responseData;
        }

        //Response Database Actions
        public List<ResponseDataModel> GetResponseData(int? responseID = null)
        {
            List<ResponseDataModel> responseData = new List<ResponseDataModel>();

            string SqlQuery = "SELECT Response.ResponseID, Survey.survey_name, Survey.type, Survey.description, Users.user_name, Response.responseCSV, Response.date " +
                "FROM Survey " +
                "INNER JOIN Response on Response.SurveyID = Survey.surveyID " +
                "INNER JOIN Users on Users.userID = Survey.userID ";

            try
            {
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
                        if (reader.HasRows)
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
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return responseData;
        }

        public bool AddNewResponse(BaseResponseModel response)
        {
            bool result = false;

            if (response != null)
            {
                string SqlQuery = "INSERT INTO Response (userID, surveyID, responseCSV, date) VALUES (@userID, @surveyID, @responseCSV, @date)";

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
                            command.Parameters.AddWithValue("@userID", response.UserID);
                            command.Parameters.AddWithValue("@surveyID", response.SurveyID);
                            command.Parameters.AddWithValue("@responseCSV", response.ResponseCSV = response.ResponseCSV ?? "");
                            command.Parameters.AddWithValue("@date", response.Date);

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

        public bool UpdateResponse(BaseResponseModel response)
        {
            bool result = false;

            if (response != null)
            {
                string SqlQuery = "UPDATE Response SET UserID = @UserID, SurveyID = @SurveyID, ResponseCSV = @ResponseCSV, Date = @Date WHERE ResponseID = @ResponseID";

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
                            command.Parameters.AddWithValue("@UserID", response.UserID);
                            command.Parameters.AddWithValue("@SurveyID", response.SurveyID);
                            command.Parameters.AddWithValue("@ResponseCSV", response.ResponseCSV = response.ResponseCSV ?? "");
                            command.Parameters.AddWithValue("@Date", response.Date);
                            command.Parameters.AddWithValue("@ResponseID", response.ResponseID);
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

        public bool DeleteResponse(int ID)
        {
            bool result = false;

            string SqlQuery = "DELETE FROM Response WHERE ResponseID = @ResponseID";

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
                        command.Parameters.AddWithValue("@ResponseID", ID);
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

        public BaseResponseModel GetSurveyFilename(int surveyID)
        {
            BaseResponseModel responseData = new BaseResponseModel();

            string SqlQuery = "SELECT ResponseID, UserID, SurveyID, ResponseCSV, Date FROM Response";

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = CONNECTION_STRING;
                    conn.Open();

                    SqlCommand command;


                    SqlQuery += " Where SurveyID = @0";
                    command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", surveyID));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                responseData = new BaseResponseModel()
                                {
                                    ResponseID = Convert.ToInt32(reader[0]),
                                    UserID = Convert.ToInt32(reader[1]),
                                    SurveyID = Convert.ToInt32(reader[2]),
                                    ResponseCSV = reader[3].ToString(),
                                    Date = Convert.ToDateTime(reader[4])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return responseData;
        }
    }
}
