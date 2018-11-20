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
	public class UserController : Controller
	{

		String baseAddress = "https://projectwebapis.azurewebsites.net/";
		String requestUri = null;

		APIService api = new APIService();

		// GET: User
		public ActionResult Index()
		{
			requestUri = "api/user";
			var response = api.GetResponseAsync(baseAddress, requestUri);

			return View(JsonConvert.DeserializeObject<List<UserDataModel>>(response.Result.Content.ReadAsAsync<string>().Result));
		}

		// GET: User/Details/5
		public ActionResult Details(int id)
		{
			requestUri = "api/user/" + id.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);

			return View(JsonConvert.DeserializeObject<List<UserDataModel>>(response.Result.Content.ReadAsAsync<string>().Result));
		}

		// GET: User/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: User/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(IFormCollection collection)
		{
			string username = collection["Username"];
			string title = collection["Title"];
			string firstName = collection["Firstname"];
			string lastName = collection["Lastname"];
			string email = collection["Email"];
			string password = collection["Password"];
			string type = collection["Type"];
			string permission = collection["Permission"];
			string groups = collection["Groups"];

			var user = new UserDataModel { Username = username, Title = title, Firstname = firstName, Lastname = lastName,
							Email = email, Password = password, Type = type, Permission = permission, Groups = groups };
			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PostAsJsonAsync("api/user/save", user);
			response.EnsureSuccessStatusCode();

			return RedirectToAction("Index", "User");
		}

		// GET: User/Edit/5
		public ActionResult Edit(int userID)
		{
			requestUri = "api/user" + userID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<UserDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			var user = list.Where(o => o.UserID == userID).ElementAt(0);
		
			return View(user);
		}

		// POST: User/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAsync(int userID, IFormCollection collection)
		{
			string username = collection["Username"];
			string title = collection["Title"];
			string firstName = collection["Firstname"];
			string lastName = collection["Lastname"];
			string email = collection["Email"];
			string password = collection["Password"];
			string type = collection["Type"];
			string permission = collection["Permission"];
			string groups = collection["Groups"];


			var user = new UserDataModel
			{
				Username = username,
				Title = title,
				Firstname = firstName,
				Lastname = lastName,
				Email = email,
				Password = password,
				Type = type,
				Permission = permission,
				Groups = groups
			};
			api.Client().BaseAddress = new Uri("http://localhost:61081/");
			HttpResponseMessage response = await api.Client().PutAsJsonAsync("api/user/" + userID.ToString(), user);
			response.EnsureSuccessStatusCode();

			return RedirectToAction("Index", "User");
		}

		// GET: User/Delete/5
		public ActionResult Delete(int userID)
		{
			requestUri = "api/user/" + userID.ToString();
			var response = api.GetResponseAsync(baseAddress, requestUri);
			var list = JsonConvert.DeserializeObject<List<UserDataModel>>(response.Result.Content.ReadAsAsync<string>().Result);

			return View(list.Where(o => o.UserID == userID).ElementAt(0));
		}

		// POST: User/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Delete(int id, IFormCollection collection)
		{
			api.Client().BaseAddress = new Uri(baseAddress);
			string userID = collection["userID"];
			HttpResponseMessage response = await api.Client().DeleteAsync("api/user/" + userID);
			response.EnsureSuccessStatusCode();


			return RedirectToAction("Index", "User");
		}
	}
}