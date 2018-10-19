using ProjectWebAPI.Models.ReportModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Services
{
    public class ReportService : DatabaseService
    {
        public List<ReportDataModel> GetReportData(int? reportID = null)
        {
            List<ReportDataModel> surveyData = new List<ReportDataModel>();
            string SqlQuery = "SELECT reportID, responseID, Name, Report_file, Date FROM Report";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNECTION_STRING;
                conn.Open();

                SqlCommand command;

                //If a parameter (Report) is passed, return data for that record else return ALL data.
                if (reportID.HasValue)
                {
                    SqlQuery += " Where ReportID = @0";
                    command = new SqlCommand(SqlQuery, conn);
                    command.Parameters.Add(new SqlParameter("0", reportID));
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
                            surveyData.Add(new ReportDataModel()
                            {
                                ReportID = Convert.ToInt32(reader[0]),
                                ResponseID = Convert.ToInt32(reader[1]),
                                Name = reader[2].ToString(),
                                ReportFile = reader[3].ToString(),
                                Date = Convert.ToDateTime(reader[4])

                            });
                        }
                    }
                }
            }

            return surveyData;
        }
    }
}
