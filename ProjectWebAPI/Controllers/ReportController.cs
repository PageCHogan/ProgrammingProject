using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models.ReportModels;
using Newtonsoft.Json;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : Controller
    {
        ReportService reportService = new ReportService();
        JsonHelper jsonHelper = new JsonHelper();

        // GET api/report
        [HttpGet]
        public string Get()
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            reportData = reportService.GetReports();

            if(reportData.Count > 0)
                result = JsonConvert.SerializeObject(reportData);

            return result;
        }

        // GET api/report/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            reportData = reportService.GetReports(id);

            if(reportData.Count > 0)
                result = JsonConvert.SerializeObject(reportData);

            return result;
        }

        // POST: api/Report
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "save":
                        response = AddNewReport(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with user?
                        break;
                    default:
                        break;
                }
            }

            return response;
        }

        // PUT: api/Report/5
        [HttpPut("{id}")]
        public string Put(int ID, [FromBody]object data)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0 && data != null)
            {
                response = UpdateReport(data, ID);
            }

            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int ID)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0)
            {
                response = DeleteReport(ID);
            }

            return response;
        }

        private string AddNewReport(object data)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            ReportDataModel newReport = jsonHelper.FromJson<ReportDataModel>(data.ToString());
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            if (reportService.AddNewReport(newReport))
            {
                result = "Successfully added new report";
            }
            else
            {
                result = "Error - Report not added";
            }
            
            return result;
        }

        private string UpdateReport(object data, int ID)
        {
            ReportDataModel report = jsonHelper.FromJson<ReportDataModel>(data.ToString());
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            string result = "Error - No changes made";

            List<ReportDataModel> existingReports = reportService.GetReports(); //Create list of existing reports
            ReportDataModel reportMatch = new ReportDataModel();

            if (existingReports != null)
            {
                reportMatch = existingReports.Find(o => o.ReportID.Equals(ID));

                if (reportMatch != null) //If report is found
                {
                    report.ReportID = reportMatch.ReportID;

                    if (reportService.UpdateReport(report))
                    {
                        result = "Successfully updated report";
                    }
                }
            }

            return result;
        }

        private string DeleteReport(int ID)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (reportService.DeleteReport(ID))
                result = "Successfully deleted report";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
