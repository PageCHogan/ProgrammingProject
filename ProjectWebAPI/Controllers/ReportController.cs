using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models.ReportModels;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : Controller
    {
        ReportService reportService = new ReportService();

        // GET api/report
        [HttpGet]
        public string Get()
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "";

            reportData = reportService.GetReports();

            result = JsonConvert.SerializeObject(reportData);
            return result;
        }

        // GET api/report/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "";

            reportData = reportService.GetReports(id);

            result = JsonConvert.SerializeObject(reportData);

            return result;
        }

        // POST: api/Report
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            //HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            string response = "";

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
            string response = "";

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
            string response = "";

            if (ID > 0)
            {
                response = DeleteReport(ID);
            }

            return response;
        }

        private string AddNewReport(object data)
        {
            ReportDataModel newReport = JsonConvert.DeserializeObject<ReportDataModel>(data.ToString());
            string result = "";

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
            ReportDataModel report = JsonConvert.DeserializeObject<ReportDataModel>(data.ToString());
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
            string result = "";

            if (reportService.DeleteReport(ID))
                result = "Successfully deleted report";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
