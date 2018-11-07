using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MajorProjectFrontEnd.Models;
using MajorProjectFrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MajorProjectFrontEnd.Controllers
{
	public class SurveyController : Controller
	{
		APIService api = new APIService();

		String baseAddress = "http://localhost:61081";
		String requestUri = null;

		// GET: Survey
		public ActionResult Index()
		{
			requestUri = "api/Survey";
			var response = api.GetResponseAsync(baseAddress, requestUri);

			return View(JsonConvert.DeserializeObject<List<SurveyDataModel>>(response.Result.Content.ReadAsAsync<string>().Result));
		}

		// GET: Survey/Details/5
		public ActionResult Details(int SurveyID)
		{
			requestUri = "api/Survey/" + SurveyID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<SurveyDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			return View(list.Where(o => o.SurveyID == SurveyID).ElementAt(0));
		}

		// GET: Survey/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Survey/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				// TODO: Add insert logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Survey/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Survey/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add update logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: Survey/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Survey/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				// TODO: Add delete logic here

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}