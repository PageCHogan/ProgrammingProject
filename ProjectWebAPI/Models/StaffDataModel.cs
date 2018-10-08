using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models
{
    public class StaffDataModel
    {
        public int StaffID { get; set; }
        public string Name { get; set; }
        //public string Password { get; set; }
        public string Type { get; set; }    
        public string Groups { get; set; }
        
        public StaffDataModel()
        {
            //StaffID = 0;
            Name = "";
            //Password = "";
            Type = "";
            Groups = "";
        }
    }
}
