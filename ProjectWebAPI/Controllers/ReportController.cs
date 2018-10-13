using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Report")]
    public class ReportController : Controller
    {
        DatabaseService databaseService = new DatabaseService();

        // GET api/report
        [HttpGet]
        public string Get()
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "";

            reportData = databaseService.GetReportData();

            result = JsonConvert.SerializeObject(reportData);
            return result;
        }

        //Retrieves report details when passed a reportID - Convert to action result and redirect to the view.
        // GET api/report/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<ReportDataModel> reportData = new List<ReportDataModel>();
            string result = "";

            reportData = databaseService.GetReportData(id);

            result = JsonConvert.SerializeObject(reportData);

            return result;
        }

        // POST: api/Report
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Report/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
