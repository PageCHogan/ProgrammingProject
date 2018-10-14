using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ProjectWebAPI.Models.StaffModels;

namespace ProjectWebAPI.Services
{
    public class StaffService : DatabaseService
    {
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
                    if (reader.HasRows)
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

        public bool AddNewStaff(StaffMemberModel staff)
        {
            bool result = false;

            if (staff != null)
            {
                string SqlQuery = "INSERT INTO Staff (Name,Password,Type,Permission,Groups) VALUES (@Name,@Password,@Type,@Permission,@Groups)";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = CONNECTION_STRING;
                    conn.Open();

                    SqlCommand command = new SqlCommand(SqlQuery, conn);

                    if (SqlQuery.Length > 0)
                    {
                        command = new SqlCommand(SqlQuery, conn);
                        command.Parameters.AddWithValue("@Name", staff.Name);
                        command.Parameters.AddWithValue("@Password", staff.Password);
                        command.Parameters.AddWithValue("@Type", staff.Type);
                        command.Parameters.AddWithValue("@Permission", staff.Permission);
                        command.Parameters.AddWithValue("@Groups", staff.Groups);
                    }
                    int sqlResult = command.ExecuteNonQuery();

                    if (sqlResult < 0)
                    {
                        Console.WriteLine("Error - No Bueno");
                    }
                }
            }

            return result;
        }
    }
}
