using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using ProjectWebAPI.Messages;
using ProjectWebAPI.Models.UserModels;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        //DatabaseService databaseService = new DatabaseService(); //TODO: Remove as obsolete
        UserService userService = new UserService();

        // GET api/user
        [HttpGet]
        public string Get()
        {
            List<UserDataModel> userData = new List<UserDataModel>();
            string result = "";

            userData = userService.GetUserData();

            result = JsonConvert.SerializeObject(userData);

            return result;
        }

        // Retrieves user details when passed a user ID - Convert to action result and redirect to the view.
        // GET api/user/5
        [HttpGet("{id}")]
        public string Get(int ID)
        {
            List<UserDataModel> userData = new List<UserDataModel>();
            string result = "";

            userData = userService.GetUserData(ID);

            result = JsonConvert.SerializeObject(userData);

            return result;
        }

        // POST api/user
        [HttpPost("{option}")]
        public HttpResponseMessage Post([FromBody] object data, string option)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            if (option != null && data != null)
            {
                switch(option.ToLower())
                {
                    case "save":
                        responseMessage = AddNewUser(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with user?
                        break;
                    default:
                        break;
                }
            }

            return responseMessage;
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private HttpResponseMessage AddNewUser(object data)
        {
            UserDataModel user = JsonConvert.DeserializeObject<UserDataModel>(data.ToString());
            BaseResponse result = new BaseResponse();

            List<UserDataModel> existingUsers = userService.GetUserData();

            if(existingUsers != null)
            {
                if(!existingUsers.Exists(o => o.Username == user.Username))
                {
                    if (userService.AddNewUser(user))
                    {
                        result.Status = "Success";
                    }
                }
            }
            else
            {
                //Error - User already exists
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(result.Status, System.Text.Encoding.UTF8, "application/json") };
            
            return response;
        }
    }
}
