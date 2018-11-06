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
	[Route("[controller]")]
	public class DoSurveyController : Controller
	{

		APIService api = new APIService();

		String baseAddress = "http://localhost:61081";


		// GET: DoSurvey
		[HttpGet("{surveyID}")]
		public ActionResult Index(int surveyID)
		{
			return View(getQuestionDataAsync(surveyID).Result);
		}


		public async Task<List<QuestionDataModel>> getQuestionDataAsync(int id)
		{
			// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

			var response = api.GetResponseAsync(baseAddress, "api/SurveyQuestions/" + id.ToString()).Result;
			
			String stringlist = null;

			if (response.IsSuccessStatusCode)
			{
				stringlist = response.Content.ReadAsAsync<string>().Result;

			}

			var list = JsonConvert.DeserializeObject<List<QuestionDataModel>>(stringlist);

			return list;

		}

		public async Task<ViewResult> SurveyResponseAsync()
		{
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/response/save", GetResponseModelList(Request.Form));
			response.EnsureSuccessStatusCode();


			return View("SurveyResponseAsync", GetResponseModelList(Request.Form));
		}



		private List<ResponseModel> GetResponseModelList(IFormCollection col)
		{
			int numberOfQuestions = Int32.Parse(col["numberOfQuestions"]);
			int surveyID = Int32.Parse(col["surveyID"]);
			List<ResponseModel> list = new List<ResponseModel>();

			for (int i = 1; i <= numberOfQuestions; ++i)
			{
				string questionOptions = col["question_options_" + i.ToString()];
				string questionTitle = col["question_title_" + i.ToString()];
				string questionType = col["question_type_" + i.ToString()];
				string response = col[i.ToString()];

				var responseModel = new ResponseModel
				{
					surveyID = surveyID,
					options = questionOptions,
					questionType = questionType,
					question = questionTitle,
					questionNumber = i,
					response = response
				};

				list.Add(responseModel);
			}

			return list;
		}



		/*
			// GET: DoSurvey/Details/5
			public ActionResult Details(int id)
			{
			    return View();
			}

			// GET: DoSurvey/Create
			public ActionResult Create()
			{
			    return View();
			}

			// POST: DoSurvey/Create
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

			// GET: DoSurvey/Edit/5
			public ActionResult Edit(int id)
			{
			    return View();
			}

			// POST: DoSurvey/Edit/5
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

			// GET: DoSurvey/Delete/5
			public ActionResult Delete(int id)
			{
			    return View();
			}

			// POST: DoSurvey/Delete/5
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
		*/
	}
}