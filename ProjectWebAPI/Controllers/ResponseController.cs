using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models.ResponseModels;
using ProjectWebAPI.Models.ViewModels;
using ProjectWebAPI.Services;
using Newtonsoft.Json;
using ProjectWebAPI.Models.UserModels;
using ProjectWebAPI.Models.SurveyModels;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
	public class ResponseController : Controller
	{
        ResponseService responseService = new ResponseService();
        UserService userService = new UserService();
        SurveyServices surveyService = new SurveyServices();
        JsonHelper jsonHelper = new JsonHelper();

        [HttpGet]
        public string Get()
        {
            List<BaseResponseModel> responseData = new List<BaseResponseModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            responseData = responseService.GetResponses();

            if(responseData.Count > 0)
                result = JsonConvert.SerializeObject(responseData);

            return result;
        }

        // GET api/responsedata/5
        [HttpGet("{id}/{option?}")]
        public string Get(int id, string option = "")
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && id > 0)
            {
                switch (option.ToLower())
                {
                    case "data":
                        List<ResponseDataModel> responseData = new List<ResponseDataModel>();
                        responseData = responseService.GetResponseData(id);
                        result = JsonConvert.SerializeObject(responseData);
                        break;
                    default:
                        List<BaseResponseModel> responses = new List<BaseResponseModel>();
                        responses = responseService.GetResponses(id);
                        result = JsonConvert.SerializeObject(responses);
                        break;
                }
            }

            return result;
        }

        // POST api/response
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "save":
                        response = AddNewResponse(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with responses?
                        break;
                    default:
                        break;
                }
            }

            return response;
        }

        // PUT api/response/5
        [HttpPut("{id}")]
        public string Put(int ID, [FromBody]object data)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0 && data != null)
            {
                response = UpdateResponse(data, ID);
            }

            return response;
        }

        // DELETE api/response/5
        [HttpDelete("{id}")]
        public string Delete(int ID)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0)
            {
                response = DeleteResponse(ID);
            }

            return response;
        }

        private string AddNewResponse(object data)
        {
            BaseResponseModel response = jsonHelper.FromJson<BaseResponseModel>(data.ToString());
            //BaseResponseModel response = JsonConvert.DeserializeObject<BaseResponseModel>(data.ToString());

            string result = "Error - unable to add new response record";

            List<UserDataModel> existingUsers = userService.GetUsers();
            List<SurveyDataModel> existingSurveys = surveyService.GetSurveys();
            
            if (existingUsers != null && existingSurveys != null)
            {
                if (existingUsers.Exists(o => o.UserID == response.UserID))
                {
                    if(existingSurveys.Exists(o => o.SurveyID == response.SurveyID))
                    {
                        if (responseService.AddNewResponse(response))
                        {
                            result = "Successfully added new response record";
                        }
                    }
                    else
                    {
                        result = "Survey not found - unable to add response record";
                    }
                }
                else
                {
                    result = "User not found - unable to add response record";
                }
            }

            return result;
        }

        private string UpdateResponse(object data, int ID)
        {
            BaseResponseModel response = jsonHelper.FromJson<BaseResponseModel>(data.ToString());
            //BaseResponseModel response = JsonConvert.DeserializeObject<BaseResponseModel>(data.ToString());

            string result = "Error - No changes made";

            List<BaseResponseModel> existingResponse = responseService.GetResponses();
            BaseResponseModel responseMatch = new BaseResponseModel();

            if (existingResponse != null)
            {
                responseMatch = existingResponse.Find(o => o.ResponseID.Equals(ID));

                if (responseMatch != null) //If response is found
                {
                    response.ResponseID = responseMatch.ResponseID;

                    if (responseService.UpdateResponse(response))
                    {
                        result = "Successfully updated response";
                    }
                }
            }

            return result;
        }

        private string DeleteResponse(int ID)
        {
            string result = "Error - No changes made";

            if (responseService.DeleteResponse(ID))
                result = "Successfully deleted response";

            return result;
        }
    }
}