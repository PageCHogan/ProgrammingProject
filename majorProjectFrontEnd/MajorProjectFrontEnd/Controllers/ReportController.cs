using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MajorProjectFrontEnd.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
		public ActionResult Index(string fileName)
		{
			return View();
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