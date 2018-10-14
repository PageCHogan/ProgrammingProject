using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models.ResponseModels;
using ProjectWebAPI.Models.ViewModels;
using ProjectWebAPI.Services;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
	public class ResponseController : Controller
	{
        ResponseService responseService = new ResponseService();

		[HttpGet]
        public string Get()
        {
            List<ResponseDataModel> responseData = new List<ResponseDataModel>();
            string result = "";

            responseData = responseService.GetResponseData();

            result = JsonConvert.SerializeObject(responseData);

            return result;
        }

        //Retrieves response details when passed a responseID
        // GET api/responsedata/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<ResponseDataModel> responseData = new List<ResponseDataModel>();
            string result = "";

            responseData = responseService.GetResponseData(id);

            result = JsonConvert.SerializeObject(responseData);

            return result;
        }

        // POST api/response
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/response/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/response/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}