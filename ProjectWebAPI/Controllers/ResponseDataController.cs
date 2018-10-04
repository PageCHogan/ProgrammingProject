using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectWebAPI.Models;
using ProjectWebAPI.Services;

namespace ProjectWebAPI.Controllers
{
	public class ResponseDataController : Controller
	{
		[Route("[controller]")]
		[HttpGet]
		public ActionResult Index()
		{
			DatabaseService databaseService = new DatabaseService();

			return View(databaseService.GetResponseData());
		}
	}
}