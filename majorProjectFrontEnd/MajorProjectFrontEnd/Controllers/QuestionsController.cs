using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MajorProjectFrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MajorProjectFrontEnd.Controllers
{
    public class QuestionsController : Controller
    {

	DatabaseService databaseService = new DatabaseService();
	HttpClient client = new HttpClient();
		
        // GET: Questions
	[Route("[controller]")]
        public ActionResult Index()
        {
            return View(databaseService.GetQuestionData());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int surveyID, int questionNumber)
        {

		ViewData["QuestionDetails"] = "surveyID: " + surveyID.ToString() + ", questionNumber: " + questionNumber.ToString();
			
	    return View(databaseService.GetQuestionData(surveyID).Where(o => o.QuestionNumber == questionNumber).ElementAt(0));
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
        public ActionResult Create(IFormCollection collection)
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

			return View();

	}

        // GET: Questions/Edit/5
        public ActionResult Edit(int surveyID, int questionNumber)
        {
			var question = databaseService.GetQuestionData(surveyID).Where(o => o.QuestionNumber == questionNumber).ElementAt(0);
			ViewData["Question"] = "Current question: question number: " + question.QuestionNumber + ", surveyID: " + question.SurveyID + ", question type: "
				+ question.Type + ", question options: " + question.Options + ", question title: " + question.Question;

	    return View();
        }

        // POST: Questions/Edit/5
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

        // GET: Questions/Delete/5
        public ActionResult Delete(int surveyID, int questionNumber)
        {
		return View(databaseService.GetQuestionData(surveyID).Where(o => o.QuestionNumber == questionNumber).ElementAt(0));
	}

        // POST: Questions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
			/*
		    try
		    {
			// TODO: Add delete logic here

			return RedirectToAction(nameof(Index));
		    }
		    catch
		    {
			return View();
		    }
			*/

		client.BaseAddress = new Uri("http://localhost:61081");

		client.DefaultRequestHeaders.Accept.Clear();
		client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));

		HttpResponseMessage response = await client.GetAsync("api/surveyQuestions/"+ collection["questionNumber"]+"/"+ collection["surveyID"]);

		return View();
        }
    }
}