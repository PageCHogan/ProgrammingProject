using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MajorProjectFrontEnd.Services
{
	public class APIService
	{
		HttpClient client = new HttpClient();

		public APIService()
		{
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			    new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<HttpResponseMessage> GetResponseAsync(string baseAddress, string requestUri)
		{
			client.BaseAddress = new Uri(baseAddress);

			return await client.GetAsync(requestUri);
		}

		public HttpClient Client()
		{
			return client;
		}
	}
}
