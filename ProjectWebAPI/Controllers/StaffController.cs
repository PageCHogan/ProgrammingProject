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
    public class StaffController : Controller
    {
        DatabaseService databaseService = new DatabaseService();

        // GET api/staff
        [HttpGet]
        //public IEnumerable<string> Get()
        public string Get()
        {
            List<StaffDataModel> staffData = new List<StaffDataModel>();
            string result = "";

            staffData = databaseService.GetStaffData();

            result = JsonConvert.SerializeObject(staffData);
            return result;
        }

        //Retrieves staff details when passed a staff ID - Convert to action result and redirect to the view.
        // GET api/staff/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<StaffDataModel> staffData = new List<StaffDataModel>();
            string result = "";

            staffData = databaseService.GetStaffData(id);

            result = JsonConvert.SerializeObject(staffData);

            return result;
        }

        // POST api/staff
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
    }
}
