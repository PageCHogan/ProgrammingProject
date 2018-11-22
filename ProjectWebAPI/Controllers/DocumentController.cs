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
using System.Net;
using ProjectWebAPI.Models.ResponseModels;
using ProjectWebAPI.Models.QuestionModels;
using ProjectWebAPI.Services;
using ProjectWebAPI.Models.ReportModels;
using ProjectWebAPI.Helpers;

namespace ProjectWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        private static string ACCOUNT_NAME = "sebenv2";
        private static string KEY = "+hK/4kPTevqxL+OqG60wEIaNjquLfxIXOBkyNkwXJ4g5CIEFUDNnD+2V6PW63omw5EZom6jhBLOTpwwESDd2sQ==";
        private static string DOWNLOAD_DIRECTORY = @"C:\Downloads\Temp\";

        //accessing with the name and key
        private static CloudStorageAccount storage = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(ACCOUNT_NAME, KEY), true);

        private static CloudBlobClient client = storage.CreateCloudBlobClient();
        private static readonly CloudBlobContainer CONTAINER = client.GetContainerReference("response-csv");

        ResponseService responseService = new ResponseService();
        private static JsonHelper jsonHelper = new JsonHelper();

        //returns a full list of the csv's that are stored in the blob.
        // GET: api/Document
        [HttpGet("{option}")]
        public string Get(string option)
        {
            CreateContainer().GetAwaiter().GetResult();
            string result = "Error unable to process request. Please ensure all inputs are valid.";

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
            string result = "Error unable to process request. Please ensure all inputs are valid.";

            if (option != null && data != null)
            {
                switch (option.ToLower())
                {
                    case "download":
                        result = DownloadDocument(data).GetAwaiter().GetResult() ? "Successfully downloaded file to " + DOWNLOAD_DIRECTORY : "Error - File not downloaded please try again";
                        break;
                    case "report": //For data manipulation
                        List<CSVResponse> reportData = GetSurveyResponses(data).GetAwaiter().GetResult();
                        if(reportData != null)
                        {
                            AnalyseReport(reportData);
                        } 
                        else
                        {
                            result = "Error, unable to run report.";
                        }
                        break;
                    case "saveresponse": //To append a response to csv in azure.
                        result = AppendResponse(data).GetAwaiter().GetResult() ? "Successfully added response to CSV" : "Error adding response to CSV";
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        // DELETE api/Document/{Option}
        [HttpDelete("{id}")]
        public string DeleteAsync(int ID)
        {
            string response = "Error unable to process request. Please ensure all inputs are valid.";

            if (ID > 0)
            {
                response = DeleteCSV(ID).GetAwaiter().GetResult() ? "Successfully deleted CSV" : "Error deleting CSV";
            }

            return response;
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

        private static async Task<bool> DownloadDocument(object data)
        {
            bool result = false;
            AzureBlobDocuments document = jsonHelper.FromJson<AzureBlobDocuments>(data.ToString());
            //AzureBlobDocuments document = JsonConvert.DeserializeObject<AzureBlobDocuments>(data.ToString());

            CloudAppendBlob appendBlob = CONTAINER.GetAppendBlobReference(document.Filename); // Get a reference to a blob named.

            List<AzureBlobDocuments> azureCollection = ListDocuments().GetAwaiter().GetResult();

            if (azureCollection != null)
            {
                if (azureCollection.Exists(o => o.Filename == document.Filename))
                {
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
                        System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                        //throw;
                    }
                }
            }

            return result;
        }

        private async Task<bool> AppendResponse(object data)
        {
            CSVResponseAppendModel responseToAppend = jsonHelper.FromJson<CSVResponseAppendModel>(data.ToString());
            //CSVResponseAppendModel responseToAppend = JsonConvert.DeserializeObject<CSVResponseAppendModel>(data.ToString());

            bool result = false;
            string fileName = null;

            BaseResponseModel CSVResponse = responseService.GetSurveyFilename(responseToAppend.SurveyID);

            if(CSVResponse != null)
                fileName = CSVResponse.ResponseCSV.Split("/").Last();

            try
            {
                CloudAppendBlob CSV = CONTAINER.GetAppendBlobReference(fileName);

                if (CSV.ExistsAsync().Result == false) //Create new blob file, if it does not exist.
                    await CSV.CreateOrReplaceAsync();

                await CSV.AppendTextAsync(responseToAppend.Response + System.Environment.NewLine); //Append responses to the end of data.
                result = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                result = false;
            }

            return result;
        }

        private async Task<bool> DeleteCSV(int ID)
        {
            bool result = false;
            string fileName = null;

            BaseResponseModel CSVResponse = responseService.GetSurveyFilename(ID);

            if (CSVResponse != null)
                fileName = CSVResponse.ResponseCSV.Split("/").Last();

            try
            {
                CloudAppendBlob CSV = CONTAINER.GetAppendBlobReference(fileName);

                if(CSV != null)
                {
                    await CSV.DeleteAsync();
                    result = true;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                result = false;
            }

            return result;
        }

        private static async Task<List<CSVResponse>> GetSurveyResponses(object data)
        {
            AzureBlobDocuments document = jsonHelper.FromJson<AzureBlobDocuments>(data.ToString());
            //AzureBlobDocuments document = JsonConvert.DeserializeObject<AzureBlobDocuments>(data.ToString());

            CSVResponse question = new CSVResponse();
            List<CSVResponse> csvResponse = new List<CSVResponse>();
            bool headerIgnore = true;
            string csvData = null;

            CloudAppendBlob appendBlob = CONTAINER.GetAppendBlobReference(document.Filename);

            try
            {
                csvData = await appendBlob.DownloadTextAsync();
                if(csvData != null)
                {
                    csvData = csvData.Replace("\r\n", ",-,"); //replace "\r\n" with end line character
                    csvData = csvData.Remove(csvData.Length - 1); //remove trailing "," before splitting to stop erronous final record.
                    string[] values = csvData.Split(',');

                    for (int i = 0; i < values.Count(); i++) 
                    {
                        Int32.TryParse(values[i++], out int surveyID);
                        question.SurveyID = surveyID;
                        question.Date = values[i++];
                        question.Time = values[i++];

                        int j = 1;
                        do
                        {
                            if(values[i].Equals("-"))
                            {
                                break;
                            }
                            question.Responses.Add(new QuestionResponse() { QuestionNumber = j++, Answer = values[i++] });

                        } while (i <= values.Count());

                        if(!headerIgnore)
                            csvResponse.Add(question);

                        question = new CSVResponse();
                        headerIgnore = false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
                //throw;
            }

            return csvResponse;
        }

        private ReportAnalysisModel AnalyseReport(List<CSVResponse> reportData)
        {
            ReportAnalysisModel results = new ReportAnalysisModel();
            string message = "";

            SurveyQuestionsService service = new SurveyQuestionsService();

            List<QuestionDataModel> questionData = service.GetSurveyQuestions(reportData[0].SurveyID);

            foreach(QuestionDataModel question in questionData)
            {
                results.Responses.Add(new ReportResponseAnalysis()
                {
                    QuestionNumber = question.QuestionNumber,
                    Question = question.Question,
                    Type = question.Type,
                    Message = message
                });
            }

            Dictionary<string, int> MQAnalysis = null;
            Dictionary<string, int> RangeAnalysis = null;
            List<QuestionAnalysisCollection> MQAnalysisList = new List<QuestionAnalysisCollection>();
            List<QuestionAnalysisCollection> RangeAnalysisList = new List<QuestionAnalysisCollection>();

            int questionNum = 1;

            do
            {
                MQAnalysis = new Dictionary<string, int>();
                RangeAnalysis = new Dictionary<string, int>();

                foreach (CSVResponse responseData in reportData)
                {
                    foreach (var response in responseData.Responses)
                    {
                        if (response.QuestionNumber == questionNum)
                        {
                            string questionType = questionData.Find(o => o.QuestionNumber == response.QuestionNumber).Type;

                            switch (questionType.ToLower())
                            {
                                case "mq":
                                    if (MQAnalysis.ContainsKey(response.Answer))
                                    {
                                        MQAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        MQAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "range":
                                    if (RangeAnalysis.ContainsKey(response.Answer))
                                    {
                                        RangeAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        RangeAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "text":
                                    break;
                                default:
                                    break;

                            }
                        }
                    }
                }
                if(MQAnalysis.Count > 0)
                    MQAnalysisList.Add(new QuestionAnalysisCollection { QuestionNumber = questionNum, Summary = MQAnalysis });

                if (RangeAnalysis.Count > 0)
                    RangeAnalysisList.Add(new QuestionAnalysisCollection { QuestionNumber = questionNum, Summary = RangeAnalysis });


                questionNum++;
            } while (questionNum <= questionData.Count);

            return results;
        }
    }
}
