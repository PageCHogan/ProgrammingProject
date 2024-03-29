﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ProjectWebAPI.Helpers
{
    public class JsonHelper
    {
        public string ErrorMessage = null;

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
            }

            return response;
        }

        public T FromJson<T>(object data)
        {
            ErrorMessage = null;

            T response = Activator.CreateInstance<T>();

            try
            {
                response = JsonConvert.DeserializeObject<T>(data.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
