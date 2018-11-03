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
			/*
			List<QuestionDataModel> questionData = null;

			questionData = databaseService.GetQuestionData(id);
			*/

			
			return View(getQuestionDataAsync(id).Result);
			
		}

		public async Task<List<QuestionDataModel>> getQuestionDataAsync(int id)
		{
			// https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client

			// What string do I put here?
			client.BaseAddress = new Uri("http://localhost:61081");

			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			    new MediaTypeWithQualityHeaderValue("application/json"));


			HttpResponseMessage response = await client.GetAsync("api/SurveyQuestions/" + id.ToString());

			String stringlist = null;
			
			if (response.IsSuccessStatusCode)
			{
				stringlist = response.Content.ReadAsAsync<string>().Result;
				//stringlist = "[{\"QuestionNumber\":1,\"SurveyID\":1,\"Question\":\"Have you visited a GP in Australia?\",\"Type\":\"MQ\",\"Options\":\"Yes,No\"},{\"QuestionNumber\":2,\"SurveyID\":1,\"Question\":\"What is your age?\",\"Type\":\"MQ\",\"Options\":\"Under 12 years old,12-17 years old,18-24 years old,25-34 years old,35-44 years old,45-54 years old,55-64 years old,65-74 years old,75 years or older\"},{\"QuestionNumber\":3,\"SurveyID\":1,\"Question\":\"What is your gender?\",\"Type\":\"MQ\",\"Options\":\"Male,Female\"},{\"QuestionNumber\":4,\"SurveyID\":1,\"Question\":\"Do you have children?\",\"Type\":\"MQ\",\"Options\":\"Yes,No\"},{\"QuestionNumber\":5,\"SurveyID\":1,\"Question\":\"Do you smoke?\",\"Type\":\"MQ\",\"Options\":\"Yes,No\"},{\"QuestionNumber\":6,\"SurveyID\":1,\"Question\":\"What is your Australian visa status?\",\"Type\":\"MQ\",\"Options\":\"Citizen,Permanent resident,Student visa,Working visa,Temporary visa,Humanitarian visa,Travel visa\"},{\"QuestionNumber\":7,\"SurveyID\":1,\"Question\":\"Which state do you live in?\",\"Type\":\"MQ\",\"Options\":\"Victoria,Queensland,South Australia,Western Australia,Tasmania,ACT,Northern Territory\"},{\"QuestionNumber\":8,\"SurveyID\":1,\"Question\":\"What is your level of health cover?\",\"Type\":\"MQ\",\"Options\":\"Medicare only,Medicare + health care card,Medicare + private health insurance,Do not have any\"},{\"QuestionNumber\":9,\"SurveyID\":1,\"Question\":\"How often do you visit a GP per year?\",\"Type\":\"NI\",\"Options\":\"\"},{\"QuestionNumber\":10,\"SurveyID\":1,\"Question\":\"What was the reason for your last visit to the GP?\",\"Type\":\"MQ\",\"Options\":\"Physical injury,Pain,Viral infection(flu),Mental illness,Skin problem,Disease ,Chronic illness,Other\"},{\"QuestionNumber\":11,\"SurveyID\":1,\"Question\":\"How do you make an appointment to see a doctor?\",\"Type\":\"MQ\",\"Options\":\"Phone,Internet,Visit\"},{\"QuestionNumber\":12,\"SurveyID\":1,\"Question\":\"How long does it take to get an appointment to see a doctor?\",\"Type\":\"MQ\",\"Options\":\"Within 6 hours,Within a day,Within 2 days,Within a few days,Within a week,A few weeks,Other\"},{\"QuestionNumber\":13,\"SurveyID\":1,\"Question\":\"How easy is it to make an appointment to see your preferred doctor?\",\"Type\":\"RANGE\",\"Options\":\"Easy,Hard,5\"},{\"QuestionNumber\":14,\"SurveyID\":1,\"Question\":\"Do you have enough quality time with your GP?\",\"Type\":\"RANGE\",\"Options\":\"Not enough,Enough,5\"},{\"QuestionNumber\":15,\"SurveyID\":1,\"Question\":\"How involved are you with the decision making in your health care?\",\"Type\":\"RANGE\",\"Options\":\"Not involved,Very involved,5\"},{\"QuestionNumber\":16,\"SurveyID\":1,\"Question\":\"How much do you trust your doctors advice?\",\"Type\":\"RANGE\",\"Options\":\"Not at all,Completely,5\"},{\"QuestionNumber\":17,\"SurveyID\":1,\"Question\":\"Did you get referred to a specialist?\",\"Type\":\"MQ\",\"Options\":\"Yes,No\"}]";
				//stringlist = await response.Content.ToString();
			}
			
			
			
			var list = JsonConvert.DeserializeObject<List<QuestionDataModel>>(stringlist);

			string a = null;
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
