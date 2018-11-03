using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MajorProjectFrontEnd.Models;
using MajorProjectFrontEnd.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AspNetCore.Http;

namespace MajorProjectFrontEnd.Controllers
{
	public class HomeController : Controller
	{
		DatabaseService databaseService = new DatabaseService();

		HttpClient client = new HttpClient();

		[HttpGet("{id}")]
		public IActionResult Index(int id)
		{
			List<QuestionDataModel> questionData = null;

			questionData = databaseService.GetQuestionData(id);

			return View(getQuestionDataAsync().Result);
		}

		public async Task<List<QuestionDataModel>> getQuestionDataAsync()
		{
			// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

			// What string do I put here?
			client.BaseAddress = new Uri("http://localhost:61081");

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			    new MediaTypeWithQualityHeaderValue("application/json"));

			List<QuestionDataModel> list = null;


			HttpResponseMessage response = await client.GetAsync("api/surveyQuestions");
			if (response.IsSuccessStatusCode)
			{
				list = await response.Content.ReadAsAsync<List<QuestionDataModel>>();
			}
			return list;

		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public async Task<ViewResult> SurveyResponseAsync()
		{
			client.BaseAddress = new Uri("http://localhost:61081/");

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			    new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage response = await client.PostAsJsonAsync("api/response/save", GetResponseModelList(Request.Form));
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

				var responseModel = new ResponseModel { surveyID = surveyID, options = questionOptions, questionType = questionType, 
									question = questionTitle, questionNumber = i, response = response };

				list.Add(responseModel);
			}

			return list;
		}
		[Route("ListQuestions")]
		public IActionResult ListQuestions()
		{
			var questionData = databaseService.GetQuestionData();

			return View(questionData);

		}
	}
}
