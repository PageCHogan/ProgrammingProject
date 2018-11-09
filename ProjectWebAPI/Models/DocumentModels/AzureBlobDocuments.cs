using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWebAPI.Models.DocumentModels
{
    public class AzureBlobDocuments
    {

        public string Filename { get; set; }

        public AzureBlobDocuments()
        {
            Filename = null;
        }
    }
}
