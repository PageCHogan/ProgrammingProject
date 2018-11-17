using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models.SurveyModels;
using ProjectWebAPI.Services;
using Newtonsoft.Json;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Survey")]
    public class SurveyController : Controller
    {
        SurveyServices surveyService = new SurveyServices();
        JsonHelper jsonHelper = new JsonHelper();

        // GET: api/Survey
        public string Get()
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            surveyData = surveyService.GetSurveys();

            if(surveyData.Count > 0)
                result = JsonConvert.SerializeObject(surveyData);
            
            return result;
        }

        // GET: api/Survey/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<SurveyDataModel> surveyData = new List<SurveyDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            surveyData = surveyService.GetSurveys(id);

            if(surveyData.Count > 0)
                result = JsonConvert.SerializeObject(surveyData);
            
            return result;
        }

        // POST: api/Survey
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "save":
                        result = AddNewSurvey(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with survey?
                        break;
                    default:
                        break;
                }
            }

            
            return result;
        }

        // PUT: api/Survey/5
        [HttpPut("{id}")]
        public string Put(int ID, [FromBody]object data)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0 && data != null)
            {
                result = UpdateSurvey(data, ID);
            }

            return result;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int ID)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0)
            {
                result = DeleteSurvey(ID);
            }

            return result;
        }

        private string AddNewSurvey(object data)
        {
            SurveyDataModel survey = jsonHelper.FromJson<SurveyDataModel>(data.ToString());
            //SurveyDataModel survey = JsonConvert.DeserializeObject<SurveyDataModel>(data.ToString());

            string result = "Error - No changes made";

            if (surveyService.AddNewSurvey(survey))
            {
                result = "Successfully added new survey";
            }

            return result;
        }

        private string UpdateSurvey(object data, int ID)
        {
            SurveyDataModel survey = jsonHelper.FromJson<SurveyDataModel>(data.ToString());
            //SurveyDataModel survey = JsonConvert.DeserializeObject<SurveyDataModel>(data.ToString());

            string result = "Error - No changes made";

            List<SurveyDataModel> existingSurveys = surveyService.GetSurveys(); //Create list of existing surveys
            SurveyDataModel surveyMatch = new SurveyDataModel();

            if (existingSurveys != null)
            {
                surveyMatch = existingSurveys.Find(o => o.SurveyID.Equals(ID));

                if (surveyMatch != null) //If survey is found
                {
                    survey.SurveyID = surveyMatch.SurveyID;

                    if (surveyService.UpdateSurvey(survey))
                    {
                        result = "Successfully updated survey";
                    }
                }
            }

            return result;
        }

        private string DeleteSurvey(int ID)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (surveyService.DeleteSurvey(ID))
                result = "Successfully deleted survey";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
