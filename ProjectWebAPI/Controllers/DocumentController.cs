﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using ProjectWebAPI.Models.DocumentModels;
using Newtonsoft.Json;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private static string ACCOUNT_NAME = "sebenv2";
        private static string KEY = "+hK/4kPTevqxL+OqG60wEIaNjquLfxIXOBkyNkwXJ4g5CIEFUDNnD+2V6PW63omw5EZom6jhBLOTpwwESDd2sQ==";
        private static string DOWNLOAD_DIRECTORY = "C:\\Downloads\\Temp";

        //accessing with the name and key
        private static CloudStorageAccount storage = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(ACCOUNT_NAME, KEY), true);

        private static CloudBlobClient client = storage.CreateCloudBlobClient();
        private static readonly CloudBlobContainer CONTAINER = client.GetContainerReference("response-csv");

        //returns a full list of the csv's that are stored in the blob.
        // GET: api/Document
        [HttpGet("{option}")]
        public string Get(string option)
        {
            CreateContainer().GetAwaiter().GetResult();
            string result = "";

            if (option != null)
            {
                switch (option.ToLower())
                {
                    case "list":
                        List<AzureBlobDocuments> azureDocuments = new List<AzureBlobDocuments>();
                        azureDocuments = ListDocuments().GetAwaiter().GetResult();
                        result = JsonConvert.SerializeObject(azureDocuments);
                        break;
                    
                    default:
                        break;
                }
            }

            return result;
        }

        // POST: api/Document/{Option}
        [HttpPost("{option}")]
        public string Post([FromBody]object data, string option)
        {
            string result = "";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "download":
                        //Still looking at returning an error message if an exception is thrown...
                        result = DownloadDocument(data).GetAwaiter().GetResult() ? "Successfully downloaded file to " + DOWNLOAD_DIRECTORY : "File not downloaded please try again";
                        break;
                    case "????": 
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private static async Task CreateContainer()
        {
            await CONTAINER.CreateIfNotExistsAsync();
        }

        private static async Task<List<AzureBlobDocuments>> ListDocuments()
        {
            BlobContinuationToken blobContinuationToken = null;
            int count = 0;

            List<AzureBlobDocuments> blobDocuments = new List<AzureBlobDocuments>();
            BlobResultSegment azureResult = await CONTAINER.ListBlobsSegmentedAsync(null, blobContinuationToken);

            do
            {
                blobContinuationToken = azureResult.ContinuationToken; // Get the value of the continuation token returned by the listing call.

                foreach (IListBlobItem item in azureResult.Results)
                {
                    count = item.Uri.Segments.Count() - 1;
                    blobDocuments.Add(new AzureBlobDocuments() { Filename = item.Uri.Segments[count].ToString() });
                }
            }
            while (blobContinuationToken != null); // Loop while the continuation token is not null.

            return blobDocuments;
        }

        private async Task<bool> DownloadDocument(object data)
        {
            bool result = false;
            AzureBlobDocuments document = JsonConvert.DeserializeObject<AzureBlobDocuments>(data.ToString());

            CloudAppendBlob appendBlob = CONTAINER.GetAppendBlobReference(document.Filename); // Get a reference to a blob named.

            try
            {
                new FileInfo(DOWNLOAD_DIRECTORY).Directory.Create(); //Create directory if not found

                using (var fileStream = System.IO.File.OpenWrite(DOWNLOAD_DIRECTORY + document.Filename))
                {
                    await appendBlob.DownloadToStreamAsync(fileStream);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Caught - " + ex.Message);
                throw;
            }

            return result;
        }

    }
}
