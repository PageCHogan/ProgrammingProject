using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebAPI.Models.UserModels;

namespace ProjectWebAPI.Services
{
    public class UserService : DatabaseService
    {
        public List<UserDataModel> GetUserData(int? userID = null)
        {
            List<UserDataModel> userData = new List<UserDataModel>();
            
            string SqlQuery = "SELECT UserID, user_name, title, first_name, last_name, email, type, permission, groups FROM Users";

            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = CONNECTION_STRING;
                    conn.Open();

                    SqlCommand command;

                    //If a parameter (staffID) is passed, return data for that record else return ALL data.
                    if (userID.HasValue)
                    {
                        SqlQuery += " Where UserID = @0";
                        command = new SqlCommand(SqlQuery, conn);
                        command.Parameters.Add(new SqlParameter("0", userID));
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
                                userData.Add(new UserDataModel()
                                {
                                    UserID = Convert.ToInt32(reader[0]),
                                    Username = reader[1].ToString(),
                                    Title = reader[2].ToString(),
                                    Firstname = reader[3].ToString(),
                                    Lastname = reader[4].ToString(),
                                    Email = reader[5].ToString(),
                                    Type = reader[6].ToString(),
                                    Permission = reader[7].ToString(),
                                    Groups = reader[8].ToString()
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

            return userData;
        }

        public bool AddNewUser(UserDataModel user)
        {
            bool result = false;

            if (user != null)
            {
                string SqlQuery = "INSERT INTO Users (user_name, title, first_name, last_name, email, type, permission, groups, password) VALUES (@user_name, @title, @first_name, @last_name, @email, @type, @permission, @groups, @password)";

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
                            command.Parameters.AddWithValue("@user_name", user.Username);
                            command.Parameters.AddWithValue("@title", user.Title);
                            command.Parameters.AddWithValue("@first_name", user.Firstname);
                            command.Parameters.AddWithValue("@last_name", user.Lastname);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@type", user.Type);
                            command.Parameters.AddWithValue("@permission", user.Permission);
                            command.Parameters.AddWithValue("@groups", user.Groups);
                            command.Parameters.AddWithValue("@password", user.Password);

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

        public bool UpdateUser(UserDataModel user)
        {
            bool result = false;

            if (user != null)
            {
                string SqlQuery = "UPDATE Users SET title = @title, first_name = @first_name, last_name = @last_name, email = @email, type = @type, permission = @permission, groups = @groups, password = @password WHERE UserID = " + user.UserID;

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
                            command.Parameters.AddWithValue("@title", user.Title);
                            command.Parameters.AddWithValue("@first_name", user.Firstname);
                            command.Parameters.AddWithValue("@last_name", user.Lastname);
                            command.Parameters.AddWithValue("@email", user.Email);
                            command.Parameters.AddWithValue("@type", user.Type);
                            command.Parameters.AddWithValue("@permission", user.Permission);
                            command.Parameters.AddWithValue("@groups", user.Groups);
                            command.Parameters.AddWithValue("@password", user.Password);
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

        public bool DeleteUser(int ID)
        {
            bool result = false;

            string SqlQuery = "DELETE FROM Users WHERE UserID = @userID";

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
                        command.Parameters.AddWithValue("@userID", ID);
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

            if (!result)
                Console.WriteLine("Error - record not deleted");

            return result;
        }
    }
}
