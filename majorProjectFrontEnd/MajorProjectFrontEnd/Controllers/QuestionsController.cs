using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MajorProjectFrontEnd.Models;
using MajorProjectFrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MajorProjectFrontEnd.Controllers
{
	public class QuestionsController : Controller
	{

		String baseAddress = "http://localhost:61081";
		String requestUri = null;

		APIService api = new APIService();
		
		HttpClient client = new HttpClient();

		// GET: Questions
		[Route("[controller]")]
		public ActionResult Index()
		{
			requestUri = "api/SurveyQuestions";
			var response = api.GetResponseAsync(baseAddress, requestUri);

			return View(JsonConvert.DeserializeObject<List<QuestionDataModel>>(response.Result.Content.ReadAsAsync<string>().Result));
		}

		// GET: Questions/Details/5
		public ActionResult Details(int surveyID, int questionNumber)
		{

			ViewData["QuestionDetails"] = "surveyID: " + surveyID.ToString() + ", questionNumber: " + questionNumber.ToString();

			requestUri = "api/SurveyQuestions";
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<QuestionDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			return View(list.Where(o => o.QuestionNumber == questionNumber).ElementAt(0));
		}

		// GET: Questions/Create
		public ActionResult Create()
		{
			ViewData["Question"] = "";
			return View();
		}

		// POST: Questions/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(IFormCollection collection)
		{

			/*
		    try
		    {
			// TODO: Add insert logic here

			return RedirectToAction(nameof(Index));
		    }
		    catch
		    {
			return View();
		    }
			*/
			

			ViewData["Question"] = "question number: " + collection["QuestionNumber"] + ", question: " + collection["Question"]
			+ ", surveyID: " + collection["surveyID"] + ", type: " + collection["Type"] + ", options: " + collection["Options"];

			var question = new QuestionDataModel(int.Parse(collection["QuestionNumber"]), int.Parse(collection["surveyID"]), collection["Question"],
								collection["Type"], collection["Options"]);
			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/surveyQuestions/save", question);
			response.EnsureSuccessStatusCode();

			return View();

		}

		// GET: Questions/Edit/5
		public ActionResult Edit(int surveyID, int questionNumber)
		{

			requestUri = "api/SurveyQuestions/" + surveyID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<QuestionDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			var question = list.Where(o => o.QuestionNumber == questionNumber).ElementAt(0);
			ViewData["Question"] = "Current question: question number: " + question.QuestionNumber + ", surveyID: " + question.SurveyID + ", question type: "
				+ question.Type + ", question options: " + question.Options + ", question title: " + question.Question;

			return View();
		}

		// POST: Questions/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, IFormCollection collection)
		{

			var question = new QuestionDataModel(int.Parse(collection["QuestionNumber"]), int.Parse(collection["surveyID"]), collection["Question"],
								collection["Type"], collection["Options"]);
			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PutAsJsonAsync("api/surveyQuestions/" + int.Parse(collection["QuestionNumber"]) + "/"
												+ collection["surveyID"], question);
			response.EnsureSuccessStatusCode();

			return View();

		}

		// GET: Questions/Delete/5
		public ActionResult Delete(int surveyID, int questionNumber)
		{

			requestUri = "api/SurveyQuestions/" + surveyID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<QuestionDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			return View(list.Where(o => o.QuestionNumber == questionNumber).ElementAt(0));
		}

		// POST: Questions/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			api.Client().BaseAddress = new Uri(baseAddress);

			HttpResponseMessage response = await api.Client().DeleteAsync("api/surveyQuestions/" + int.Parse(collection["questionNumber"]) + "/"
												+ collection["surveyID"]);
			response.EnsureSuccessStatusCode();


			return RedirectToAction("Index", "Questions");
		}
	}
}