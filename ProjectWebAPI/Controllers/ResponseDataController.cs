using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models.ViewModels;
using ProjectWebAPI.Services;

namespace ProjectWebAPI.Controllers
{
	public class ResponseDataController : Controller
	{
        [Route("api/[controller]")]
		[HttpGet]
		public ActionResult Index()

        {
			DatabaseService databaseService = new DatabaseService();

            ResponseDataViewModel model = new ResponseDataViewModel();

            model.Responses = databaseService.GetResponseData();

			return View(model);
		}
	}
}