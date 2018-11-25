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
using ProjectWebAPI.Models.ReportModels;

namespace MajorProjectFrontEnd.Controllers
{
	[Route("[controller]")]
	public class ReportController : Controller
	{

		String baseAddress = "https://projectwebapis.azurewebsites.net/";
		String requestUri = null;

		APIService api = new APIService();

		// GET: Report

		[HttpGet("{fileName}")]
		public async Task<ActionResult> Index(string fileName)
		{
			requestUri = "api/Document/report";
			api.Client().BaseAddress = new Uri(baseAddress);
			HttpResponseMessage response = await api.Client().PostAsJsonAsync<ReportModel>(requestUri, new ReportModel { Filename = fileName });

			string responseString = await response.Content.ReadAsAsync<string>();

			
return View(JsonConvert.DeserializeObject<ReportAnalysisModel>(responseString));
		}

		// GET: Report/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: Report/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Report/Create
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

		// GET: Report/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: Report/Edit/5
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

		// GET: Report/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: Report/Delete/5
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