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

		String baseAddress = "https://projectwebapis.azurewebsites.net/";
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
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			int SurveyID = int.Parse(collection["SurveyID"]);
			string SurveyName = collection["SurveyName"];
			int UserID = int.Parse(collection["UserID"]);
			string Type = collection["Type"];
			string Description = collection["Description"];
			DateTime StartDate = Convert.ToDateTime(collection["StartDate"]);
			DateTime EndDate = Convert.ToDateTime(collection["EndDate"]);
			string Permission = collection["Permission"];

			var survey = new SurveyDataModel
			{
				SurveyID = SurveyID,
				SurveyName = SurveyName,
				UserID = UserID,
				Type = Type,
				Description = Description,
				StartDate = StartDate,
				EndDate = EndDate,
				Permission = Permission
			};

			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/survey/save", survey);
			response.EnsureSuccessStatusCode();

			return View();
		}

		// GET: Survey/Edit/5
		public ActionResult Edit(int SurveyID)
		{
			requestUri = "api/survey/" + SurveyID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<SurveyDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			var survey = list.Where(o => o.SurveyID == SurveyID).ElementAt(0);

			ViewData["Survey"] = survey.ToString();

			return View();
		}

		// POST: Survey/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int SurveyID, IFormCollection collection)
		{
			string SurveyName = collection["SurveyName"];
			int UserID = int.Parse(collection["UserID"]);
			string Type = collection["Type"];
			string Description = collection["Description"];
			DateTime StartDate = Convert.ToDateTime(collection["StartDate"]);
			DateTime EndDate = Convert.ToDateTime(collection["EndDate"]);
			string Permission = collection["Permission"];

			var survey = new SurveyDataModel
			{
				SurveyID = SurveyID,
				SurveyName = SurveyName,
				UserID = UserID,
				Type = Type,
				Description = Description,
				StartDate = StartDate,
				EndDate = EndDate,
				Permission = Permission
			};

			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/survey/" + SurveyID.ToString(), survey);
			response.EnsureSuccessStatusCode();

			return View();
		}

		// GET: Survey/Delete/5
		public ActionResult Delete(int SurveyID)
		{
			requestUri = "api/Survey/" + SurveyID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<SurveyDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			return View(list.Where(o => o.SurveyID == SurveyID).ElementAt(0));
		}

		// POST: Survey/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			api.Client().BaseAddress = new Uri(baseAddress);

			HttpResponseMessage response = await api.Client().DeleteAsync("api/survey/" + collection["surveyID"]);
			response.EnsureSuccessStatusCode();


			return RedirectToAction("Index", "Questions");
		}
	}
}