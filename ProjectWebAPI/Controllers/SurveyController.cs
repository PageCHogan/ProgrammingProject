using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;
using ProjectWebAPI.Services;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Survey")]
    public class SurveyController : Controller
    {
        DatabaseService databaseService = new DatabaseService();

        // GET: api/Survey
        public string Get()
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string result = "";

            surveyData = databaseService.GetSurveyData();

            result = JsonConvert.SerializeObject(surveyData);
            return result;
        }

        // GET: api/Survey/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string result = "";

            surveyData = databaseService.GetSurveyData(id);

            result = JsonConvert.SerializeObject(surveyData);

            return result;
        }

        // POST: api/Survey
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Survey/5
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
