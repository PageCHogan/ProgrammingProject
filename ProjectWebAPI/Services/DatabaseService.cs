using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectWebAPI.Services
{
	public class DatabaseService
	{
		public const string CONNECTION_STRING = "Data Source=sebens.database.windows.net;Initial Catalog=Seben;Persist Security Info=True;User ID=sebens;Password=EveryoneHD!";

		public DatabaseService()
		{
			//Empty Constructor...for now
		}
    }
}
