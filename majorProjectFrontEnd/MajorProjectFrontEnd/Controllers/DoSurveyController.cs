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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ViewResult> SurveyResponseAsync()
		{
			api.Client().BaseAddress = new Uri(baseAddress);
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/document/saveresponse", GetCSVModel(Request.Form));
			response.EnsureSuccessStatusCode();
			
			

			return View("SurveyResponseAsync", GetResponseModelList(Request.Form));
		}

		
		private CSVModel GetCSVModel(IFormCollection col)
		{
			int numberOfQuestions = Int32.Parse(col["numberOfQuestions"]);
			int surveyID = Int32.Parse(col["surveyID"]);
			var currentTime = DateTime.Now;
			string year = currentTime.Year.ToString();

			// https://stackoverflow.com/questions/1152583/cdatetime-now-month-output-format
			string month = currentTime.Month.ToString("d2");
			string day = currentTime.Day.ToString("d2");

			string date = year + month + day;

			string time = currentTime.ToString("H:mm:ss");

			string ResponseCSV = "";

			ResponseCSV += (surveyID.ToString() + "," + date + "," + time);

			for (int i = 1; i <= numberOfQuestions; ++i)
			{
				ResponseCSV += ",";
				ResponseCSV += col[i.ToString()].ToString().Replace(',', '|');

            }

			return new CSVModel { ResponseCSV = ResponseCSV, SurveyID = surveyID };
		}


		private List<ResponseModel> GetResponseModelList(IFormCollection col)
		{
			int numberOfQuestions = Int32.Parse(col["numberOfQuestions"]);
			int surveyID = Int32.Parse(col["surveyID"]);
			List<ResponseModel> list = new List<ResponseModel>();

			var currentTime = DateTime.Now;
			string year = currentTime.Year.ToString();

			// https://stackoverflow.com/questions/1152583/cdatetime-now-month-output-format
			string month = currentTime.Month.ToString("d2");
			string day = currentTime.Day.ToString("d2");

			string date = year + month + day;

			string time = currentTime.ToString("H:mm:ss");

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
					response = response,
					date = date,
					time = time
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