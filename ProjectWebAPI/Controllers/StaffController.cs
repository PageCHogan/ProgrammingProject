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
using ProjectWebAPI.Models.StaffModels;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StaffController : Controller
    {
        //DatabaseService databaseService = new DatabaseService(); //TODO: Remove as obsolete
        StaffService staffService = new StaffService();

        // GET api/staff
        [HttpGet]
        public string Get()
        {
            List<StaffDataModel> staffData = new List<StaffDataModel>();
            string result = "";

            staffData = staffService.GetStaffData();

            result = JsonConvert.SerializeObject(staffData);

            return result;
        }

        // Retrieves staff details when passed a staff ID - Convert to action result and redirect to the view.
        // GET api/staff/5
        [HttpGet("{id}")]
        public string Get(int ID)
        {
            List<StaffDataModel> staffData = new List<StaffDataModel>();
            string result = "";

            staffData = staffService.GetStaffData(ID);

            result = JsonConvert.SerializeObject(staffData);

            return result;
        }

        // POST api/staff
        [HttpPost("{option}")]
        public HttpResponseMessage Post([FromBody] object data, string option)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            if (option != null && data != null)
            {
                switch(option.ToLower())
                {
                    case "save":
                        responseMessage = AddNewStaff(data);
                        break;
                    case "????": //TODO: Any other options required for interacting with staff?
                        break;
                    default:
                        break;
                }
            }

            return responseMessage;
        }

        // PUT api/staff/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/staff/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private HttpResponseMessage AddNewStaff(object data)
        {
            StaffMemberModel staff = JsonConvert.DeserializeObject<StaffMemberModel>(data.ToString());
            BaseResponse result = new BaseResponse();

            List<StaffDataModel> existingStaff = staffService.GetStaffData();

            if(existingStaff != null)
            {
                if(!existingStaff.Exists(o => o.Name == staff.Name))
                {
                    if (staffService.AddNewStaff(staff))
                    {
                        result.Status = "Success";
                    }
                }
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(result.Status, System.Text.Encoding.UTF8, "application/json") };
            
            return response;
        }
    }
}
