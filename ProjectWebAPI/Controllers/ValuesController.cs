using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        DatabaseService databaseService = new DatabaseService();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<StaffTestDataModel> test = new List<StaffTestDataModel>();
            string result = "";
            bool init = false;

            test = databaseService.GetTestData();
            
            foreach (StaffTestDataModel staff in test)
            {
                if(!init)
                {
                    result += " ";
                    init = true;
                }

                result += staff.Name + " / ";
            }

            yield return result.ToString();
            //new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            return "value selected: " + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
