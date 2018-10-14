using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models.QuestionModels;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SurveyQuestionsController : Controller
    {
        //DatabaseService databaseService = new DatabaseService();
        SurveyQuestionsService surveyQuestionService = new SurveyQuestionsService();

        // GET api/surveyQuestions
        [HttpGet]
        public string Get()
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string result = "";

            questionData = surveyQuestionService.GetQuestionData();

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

            questionData = surveyQuestionService.GetQuestionData(id);

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
