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
        UserService userService = new UserService();

        // GET api/user
        [HttpGet]
        public string Get()
        {
            string result;

            List<UserDataModel> userData = new List<UserDataModel>();
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
        public string Post([FromBody]object data, string option)
        {
            //HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
            string response = "";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "save":
                        response = AddNewUser(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with user?
                        break;
                    default:
                        break;
                }
            }

            return response;
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public string Put(int ID, [FromBody]object data)
        {
            string response = "";

            if (ID > 0 && data != null)
            {
                response = UpdateUser(data, ID);
            }

            return response;
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public string Delete(int ID)
        {
            string response = "";

            if (ID > 0)
            {
                response = DeleteUser(ID);
            }

            return response;
        }

        private string AddNewUser(object data)
        {
            UserDataModel user = JsonConvert.DeserializeObject<UserDataModel>(data.ToString());
            string result = "Error - User already exists";

            List<UserDataModel> existingUsers = userService.GetUserData();

            if (existingUsers != null)
            {
                if (!existingUsers.Exists(o => o.Username == user.Username))
                {
                    if (userService.AddNewUser(user))
                    {
                        result = "Successfully added new user";
                    }
                }
            }

            return result;
        }

        private string UpdateUser(object data, int ID)
        {
            UserDataModel user = JsonConvert.DeserializeObject<UserDataModel>(data.ToString());
            string result = "Error - No changes made";

            List<UserDataModel> existingUsers = userService.GetUserData(); //Create list of existing users
            UserDataModel userMatch = new UserDataModel();

            if (existingUsers != null)
            {
                userMatch = existingUsers.Find(o => o.UserID.Equals(ID));

                if (userMatch != null) //If user is found
                {
                    user.UserID = userMatch.UserID;

                    if (userService.UpdateUser(user))
                    {
                        result = "Successfully updated user";
                    }
                }
            }

            return result;
        }

        private string DeleteUser(int ID)
        {
            string result = "";

            if (userService.DeleteUser(ID))
                result = "Successfully deleted user";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
