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
    }
}
