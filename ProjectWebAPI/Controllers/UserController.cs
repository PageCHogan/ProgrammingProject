﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using Newtonsoft.Json;
using ProjectWebAPI.Models.UserModels;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserService userService = new UserService();
        JsonHelper jsonHelper = new JsonHelper();

        // GET api/user
        [HttpGet]
        public string Get()
        {
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            List<UserDataModel> userData = new List<UserDataModel>();
            userData = userService.GetUsers();

            if(userData.Count > 0)
                result = JsonConvert.SerializeObject(userData);


            return result;
        }

        // Retrieves user details when passed a user ID - Convert to action result and redirect to the view.
        // GET api/user/5
        [HttpGet("{id}")]
        public string Get(int ID)
        {
            List<UserDataModel> userData = new List<UserDataModel>();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            userData = userService.GetUsers(ID);

            if(userData.Count > 0)
                result = JsonConvert.SerializeObject(userData);

            return result;
        }

        // POST api/user
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
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
            string response = "Error unable to process request. Please ensure all inputs are valid.";

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
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0)
            {
                response = DeleteUser(ID);
            }

            return response;
        }

        private string AddNewUser(object data)
        {
            UserDataModel user = jsonHelper.FromJson<UserDataModel>(data.ToString());
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            string result = "Error - User already exists";

            List<UserDataModel> existingUsers = userService.GetUsers();

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
            UserDataModel user = jsonHelper.FromJson<UserDataModel>(data.ToString());
            if (!string.IsNullOrEmpty(jsonHelper.ErrorMessage))
                return jsonHelper.ErrorMessage;

            string result = "Error - No changes made";

            List<UserDataModel> existingUsers = userService.GetUsers(); //Create list of existing users
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
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (userService.DeleteUser(ID))
                result = "Successfully deleted user";
            else
                result = "Error - No changes made";

            return result;
        }
    }
}
