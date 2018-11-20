using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models.QuestionModels;
using ProjectWebAPI.Models.SurveyModels;
using Newtonsoft.Json;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SurveyQuestionsController : Controller
    {
        SurveyQuestionsService surveyQuestionService = new SurveyQuestionsService();
        SurveyServices surveyService = new SurveyServices();
        JsonHelper jsonHelper = new JsonHelper();

        // GET api/surveyQuestions
        [HttpGet]
        public string Get()
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            questionData = surveyQuestionService.GetSurveyQuestions();

            if(questionData.Count > 0)
                result = JsonConvert.SerializeObject(questionData);

            return result;
        }

        //Retrieves question details when passed a surveyID
        // GET api/surveyQuestions/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<QuestionDataModel> questionData = new List<QuestionDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            questionData = surveyQuestionService.GetSurveyQuestions(id);

            if(questionData.Count > 0)
                result = JsonConvert.SerializeObject(questionData);

            return result;
        }

        // POST api/surveyQuestions
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "save":
                        response = AddNewQuestion(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with questions?
                        break;
                    default:
                        break;
                }
            }

            return response;
        }

        // PUT api/surveyQuestions/5
        [HttpPut("{questionID}/{surveyID}")]
        public string Put(int questionID, int surveyID, [FromBody]object data)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if ((questionID > 0 && surveyID > 0) && data != null)
            {
                response = UpdateQuestion(data, questionID, surveyID);
            }

            return response;
        }

        // DELETE api/surveyQuestions/5
        [HttpDelete("{questionID}/{surveyID}")]
        public string Delete(int questionID, int surveyID)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (questionID > 0 && surveyID > 0)
            {
                response = DeleteQuestion(questionID, surveyID);
            }

            return response;
        }

        private string AddNewQuestion(object data)
        {
            string result = "Entered survey not found, failed to add question";

            QuestionDataModel question = jsonHelper.FromJson<QuestionDataModel>(data);
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            List<SurveyDataModel> existingSurveys = surveyService.GetSurveys();

            if(existingSurveys != null)
            {
                if(existingSurveys.Exists(o => o.SurveyID == question.SurveyID))
                {
                    if (surveyQuestionService.AddNewQuestion(question))
                    {
                        result = "Successfully added new question";
                    }
                    else
                    {
                        result = "Error - Failed to add new question";
                    }
                }
            }

            return result;
        }

        private string UpdateQuestion(object data, int questionID, int surveyID)
        {
            QuestionDataModel question = jsonHelper.FromJson<QuestionDataModel>(data.ToString());
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            string result = "Entered survey not found, failed to update question";

            List<SurveyDataModel> existingSurveys = surveyService.GetSurveys();

            if (existingSurveys != null)
            {
                if (existingSurveys.Exists(o => o.SurveyID == question.SurveyID))
                {
                    if (surveyQuestionService.UpdateQuestion(question))
                    {
                        result = "Successfully updated question";
                    }
                    else
                    {
                        result = "Error - No changes made";
                    }
                }
            }

            return result;
        }

        private string DeleteQuestion(int questionID, int surveyID)
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (surveyQuestionService.DeleteQuestion(questionID, surveyID))
                result = "Successfully deleted question";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
