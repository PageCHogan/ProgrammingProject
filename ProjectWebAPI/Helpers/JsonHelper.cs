using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Helpers
{
    public class JsonHelper
    {

        public List<T> FromJsonList<T>(object data)
        {
            List<T> response = null;

            try
            {
                response = JsonConvert.DeserializeObject<List<T>>(data.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                //throw;
            }

            return response;
        }

        public T FromJson<T>(object data)
        {
            T response;

            try
            {
                response = JsonConvert.DeserializeObject<T>(data.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                throw;
            }

            return response;
        }
    }
}
