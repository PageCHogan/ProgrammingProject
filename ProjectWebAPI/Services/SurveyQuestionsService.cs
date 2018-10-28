using ProjectWebAPI.Models.QuestionModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Services
{
    public class SurveyQuestionsService : DatabaseService
    {
        //Returns all questions in database OR if surveyID provided, will return all questions belonging to that survey.
        public List<QuestionDataModel> GetSurveyQuestions(int? surveyID = null)
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string SqlQuery = "SELECT QuestionNumber, surveyID, question, type, options FROM Questions";

            try
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Caught - " + ex.Message);
                throw;
            }

            return questionData;
        }

        //Not Currently Working
        public bool AddNewQuestion(QuestionDataModel question)
        {
            bool result = false;

            if (question != null)
            {
                string SqlQuery = "INSERT INTO Questions (SurveyID, QuestionNumber, Question, Type, Options) VALUES (@SurveyID, @QuestionNumber, @Question, @Type, @Options)";

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
                            command.Parameters.AddWithValue("@SurveyID", question.SurveyID);
                            command.Parameters.AddWithValue("@QuestionNumber", question.QuestionNumber);
                            command.Parameters.AddWithValue("@Question", question.Question);
                            command.Parameters.AddWithValue("@Type", question.Type);
                            command.Parameters.AddWithValue("@Options", question.Options);
                        }
                        int sqlResult = command.ExecuteNonQuery();

                        result = sqlResult < 0 ? false : true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Caught - " + ex.Message);
                    throw;
                }
            }

            if (!result)
                Console.WriteLine("Error - record not saved to database");

            return result;
        }

        public bool UpdateQuestion(QuestionDataModel question)
        {
            bool result = false;

            if (question != null)
            {
                string SqlQuery = "UPDATE Questions SET Question = @Question, Type = @Type, Options = @Options  WHERE SurveyID = @SurveyID AND QuestionNumber = @QuestionNumber";

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
                            command.Parameters.AddWithValue("@SurveyID", question.SurveyID);
                            command.Parameters.AddWithValue("@QuestionNumber", question.QuestionNumber);
                            command.Parameters.AddWithValue("@Question", question.Question);
                            command.Parameters.AddWithValue("@Type", question.Type);
                            command.Parameters.AddWithValue("@Options", question.Options);
                        }
                        int sqlResult = command.ExecuteNonQuery();

                        result = sqlResult < 0 ? false : true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Caught - " + ex.Message);
                    throw;
                }
            }

            if (!result)
                Console.WriteLine("Error - record not updated in database");

            return result;
        }

        public bool DeleteQuestion(int questionID, int surveyID)
        {
            bool result = false;

            string SqlQuery = "DELETE FROM Questions WHERE QuestionNumber = @QuestionNumber AND SurveyID = @SurveyID";

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
                        command.Parameters.AddWithValue("@SurveyID", surveyID);
                        command.Parameters.AddWithValue("@QuestionNumber", questionID);
                    }

                    int sqlResult = command.ExecuteNonQuery();

                    result = sqlResult < 0 ? false : true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (!result)
                Console.WriteLine("Error - record not deleted");

            return result;
        }
    }
}
