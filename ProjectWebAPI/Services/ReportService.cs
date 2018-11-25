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
        public List<ReportDataModel> GetReports(int? reportID = null)
        {
            List<ReportDataModel> surveyData = new List<ReportDataModel>();
            string SqlQuery = "SELECT reportID, responseID, Name, Report_file, Date FROM Report";

            try
            {
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
            }
            catch (Exception ex )
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return surveyData;
        }

        public bool AddNewReport(ReportDataModel report)
        {
            bool result = false;

            if (report != null)
            {
                if(report.Date == null || report.Date == DateTime.MinValue)
                    report.Date = DateTime.Now;

                string SqlQuery = "INSERT INTO Report (responseID, Name, Report_file, Date) VALUES (@responseID, @Name, @Report_file, @Date)";

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
                            command.Parameters.AddWithValue("@responseID", report.ResponseID);
                            command.Parameters.AddWithValue("@Name", report.Name = report.Name ?? "");
                            command.Parameters.AddWithValue("@Report_file", report.ReportFile = report.ReportFile ?? "");
                            command.Parameters.AddWithValue("@Date", report.Date);

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

        public bool UpdateReport(ReportDataModel report)
        {
            bool result = false;

            if (report != null)
            {
                if (report.Date == null || report.Date == DateTime.MinValue)
                    report.Date = DateTime.Now;

                string SqlQuery = "UPDATE Report SET name = @name, report_file = @report_file, Date = @Date WHERE ReportID = " + report.ReportID;

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
                            command.Parameters.AddWithValue("@name", report.Name = report.Name ?? "");
                            command.Parameters.AddWithValue("@report_file", report.ReportFile = report.ReportFile ?? "");
                            command.Parameters.AddWithValue("@Date", report.Date);
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

        public bool DeleteReport(int ID)
        {
            bool result = false;

            string SqlQuery = "DELETE FROM Report WHERE ReportID = @reportID";

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
                        command.Parameters.AddWithValue("@reportID", ID);
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
