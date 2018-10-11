using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class SurveyQuestionsController : Controller
    {
        DatabaseService databaseService = new DatabaseService();

        // GET api/surveyQuestions
        [HttpGet]
        public string Get()
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string result = "";

            questionData = databaseService.GetQuestionData();

            result = JsonConvert.SerializeObject(questionData);
            return result;
        }

        //Retrieves question details when passed a surveyID
        // GET api/surveyQuestions/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string result = "";

            questionData = databaseService.GetQuestionData(id);

            result = JsonConvert.SerializeObject(questionData);

            return result;
        }

        // POST api/surveyQuestions
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/surveyQuestions/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/surveyQuestions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
