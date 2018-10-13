using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.StaffModels
{
    public class StaffMemberModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Groups { get; set; }

        public StaffMemberModel()
        {
            Name = "";
            Type = "";
            Groups = "";
        }
    }
}
