﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;
using ProjectWebAPI.Models.ViewModels;
using ProjectWebAPI.Services;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
	public class ResponseDataController : Controller
	{
        DatabaseService databaseService = new DatabaseService();

		[HttpGet]
        public string Get()
        {
			DatabaseService databaseService = new DatabaseService();

            ResponseDataViewModel model = new ResponseDataViewModel();

            model.Responses = databaseService.GetResponseData();

            string result = JsonConvert.SerializeObject(model.Responses);

            return result;
		}

        //Retrieves response details when passed a responseID
        // GET api/responsedata/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            List<ResponseDataModel> responseData = new List<ResponseDataModel>();
            string result = "";

            responseData = databaseService.GetResponseData(id);

            result = JsonConvert.SerializeObject(responseData);

            return result;
        }
    }
}