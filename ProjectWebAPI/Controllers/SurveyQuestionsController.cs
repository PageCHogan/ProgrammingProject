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
        [HttpPut("{id}")]
        public string Put(int ID, [FromBody]object data)
        {
            string response = "";

            if (ID > 0 && data != null)
            {
                response = UpdateQuestion(data, ID);
            }

            return response;
        }

        // DELETE api/surveyQuestions/5
        [HttpDelete("{questionID}/{surveyID}")]
        public string Delete(int questionID, int surveyID)
        {
            string response = "";

            if (questionID > 0 && surveyID > 0)
            {
                response = DeleteQuestion(questionID, surveyID);
            }

            return response;
        }

        private string AddNewQuestion(object data)
        {
            QuestionDataModel question = JsonConvert.DeserializeObject<QuestionDataModel>(data.ToString());
            string result = "";

            List<QuestionDataModel> existingUsers = surveyQuestionService.GetQuestionData();

            if (surveyQuestionService.AddNewQuestion(question))
            {
                result = "Successfully added new question";
            }
            else
            {
                result = "Error - Failed to add new question";
            }

            return result;
        }

        private string UpdateQuestion(object data, int ID)
        {
            QuestionDataModel question = JsonConvert.DeserializeObject<QuestionDataModel>(data.ToString());
            string result = "";

            if (surveyQuestionService.UpdateQuestion(question))
            {
                result = "Successfully updated question";
            }
            else
            {
                result = "Error - No changes made";
            }

            return result;
        }

        private string DeleteQuestion(int questionID, int surveyID)
        {
            string result = "";

            if (surveyQuestionService.DeleteQuestion(questionID, surveyID))
                result = "Successfully deleted question";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
