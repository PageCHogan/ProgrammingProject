using Microsoft.AspNetCore.Mvc;
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
                        AzureBlobDocuments document = jsonHelper.FromJson<AzureBlobDocuments>(data.ToString());
                        if (reportData != null)
                        {
                            ReportAnalysisModel report = CollateReportData(reportData);
                            report.ReportTitle = document.Filename;
                            if(report != null)
                            {
                                result = JsonConvert.SerializeObject(report);
                            }
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
            }

            return csvResponse;
        }

        private ReportAnalysisModel CollateReportData(List<CSVResponse> reportData)
        {
            ReportAnalysisModel report = new ReportAnalysisModel();

            SurveyQuestionsService service = new SurveyQuestionsService();

            List<QuestionDataModel> questionData = service.GetSurveyQuestions(reportData[0].SurveyID);

            foreach(QuestionDataModel question in questionData)
            {
                report.Responses.Add(new ReportResponseAnalysis()
                {
                    QuestionNumber = question.QuestionNumber,
                    Question = question.Question,
                    Type = question.Type,
                    Options = question.Options
                });
            }

            Dictionary<string, int> multiChoiceAnalysis = null;
            Dictionary<string, int> rangeAnalysis = null;
            Dictionary<string, int> stringAnalysis = null;
            Dictionary<string, int> rankAnalysis = null;
            Dictionary<string, int> numericAnalysis = null;
            List<QuestionAnalysisCollection> surveyAnalysis = new List<QuestionAnalysisCollection>();

            int questionNum = 1;

            do
            {
                multiChoiceAnalysis = new Dictionary<string, int>();
                rangeAnalysis = new Dictionary<string, int>();
                stringAnalysis = new Dictionary<string, int>();
                rankAnalysis = new Dictionary<string, int>();
                numericAnalysis = new Dictionary<string, int>();

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
                                    if (multiChoiceAnalysis.ContainsKey(response.Answer))
                                    {
                                        multiChoiceAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        multiChoiceAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "range":
                                    if (rangeAnalysis.ContainsKey(response.Answer))
                                    {
                                        rangeAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        rangeAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "ni":
                                    if (numericAnalysis.ContainsKey(response.Answer))
                                    {
                                        numericAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        numericAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "rank":
                                    if (rankAnalysis.ContainsKey(response.Answer))
                                    {
                                        rankAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        rankAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                case "comment":
                                case "text":
                                    if (stringAnalysis.ContainsKey(response.Answer))
                                    {
                                        stringAnalysis[response.Answer]++;
                                    }
                                    else
                                    {
                                        stringAnalysis.Add(response.Answer, 1);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                if(multiChoiceAnalysis.Count > 0)
                    surveyAnalysis.Add(new QuestionAnalysisCollection { QuestionType = "mq", QuestionNumber = questionNum, Summary = multiChoiceAnalysis });
                else if (rangeAnalysis.Count > 0)
                    surveyAnalysis.Add(new QuestionAnalysisCollection { QuestionType = "range", QuestionNumber = questionNum, Summary = rangeAnalysis });
                else if (rankAnalysis.Count > 0)
                    surveyAnalysis.Add(new QuestionAnalysisCollection { QuestionType = "rank", QuestionNumber = questionNum, Summary = rankAnalysis });
                else if (stringAnalysis.Count > 0)
                    surveyAnalysis.Add(new QuestionAnalysisCollection { QuestionType = "text", QuestionNumber = questionNum, Summary = stringAnalysis });
                else if (numericAnalysis.Count > 0)
                    surveyAnalysis.Add(new QuestionAnalysisCollection { QuestionType = "ni", QuestionNumber = questionNum, Summary = numericAnalysis });

                questionNum++;
            } while (questionNum <= questionData.Count);

            report = AnalyseReport(report, surveyAnalysis);

            return report;
        }

        private ReportAnalysisModel AnalyseReport(ReportAnalysisModel report, List<QuestionAnalysisCollection> surveyAnalysis)
        {
            if(report != null)
            {
                foreach(ReportResponseAnalysis response in report.Responses)
                {
                    QuestionAnalysisCollection surveyResponse = surveyAnalysis.Find(o => o.QuestionNumber == response.QuestionNumber);

                    if(surveyResponse != null)
                    {
                        switch(surveyResponse.QuestionType)
                        {
                            case "mq":
                                response.Message = MultipleChoiceAnalysis(response, surveyResponse);
                                break;
                            case "ni":
                            case "range":
                                response.Message = RangeChoiceAnalysis(response, surveyResponse);
                                break;
                            case "rank":
                                response.Message = RankChoiceAnalysis(response, surveyResponse);
                                break;
                            case "text":
                                response.Message = TextChoiceAnalysis(response, surveyResponse);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return report;
        }

        private List<string> MultipleChoiceAnalysis(ReportResponseAnalysis response, QuestionAnalysisCollection surveyResponse)
        {
            List<string> result = new List<string>();
            string option = "";

            try
            {
                int sum = surveyResponse.Summary.Values.Sum();
                List<string> options = response.Options.Split(',').ToList();
                var ordered = surveyResponse.Summary.OrderBy(x => x.Key);

                for (int i = 0; i < surveyResponse.Summary.Count; i++)
                {
                    option = options.ElementAt(Int32.Parse(ordered.ElementAt(i).Key) - 1);
                    result.Add(string.Format("{0} people answered with a response of: {1}", ordered.ElementAt(i).Value.ToString(), option));
                }

                result.Add(string.Format("A total of {0} people participated in this survey.", sum));

                response.Message = result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }
            
            return result;
        }

        private List<string> RangeChoiceAnalysis(ReportResponseAnalysis response, QuestionAnalysisCollection surveyResponse)
        {
            List<string> result = new List<string>();
            double percentage = 0;

            try
            {
                double sum = surveyResponse.Summary.Values.Sum();
                var ordered = surveyResponse.Summary.OrderBy(x => x.Key);

                for (int i = 0; i < surveyResponse.Summary.Count; i++)
                {
                    percentage = Math.Round((ordered.ElementAt(i).Value / sum) * 100);
                    result.Add(string.Format("approximately {0}% of people answered with a response of: {1}", percentage, ordered.ElementAt(i).Key.ToString()));
                }

                result.Add(string.Format("A total of {0} people participated in this survey.", sum));

                response.Message = result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return result;
        }

        private List<string> RankChoiceAnalysis(ReportResponseAnalysis response, QuestionAnalysisCollection surveyResponse)
        {
            List<string> result = new List<string>();

            try
            {
                int sum = surveyResponse.Summary.Values.Sum();
                var ordered = surveyResponse.Summary.OrderBy(x => x.Value);

                result.Add(string.Format("{0} people answered with the least popular response of: {1}", ordered.First().Value.ToString(), ordered.First().Key.ToString()));
                result.Add(string.Format("{0} people answered with the most popular response of: {1}", ordered.Last().Value.ToString(), ordered.Last().Key.ToString()));
                result.Add(string.Format("A total of {0} people participated in this survey.", sum));

                response.Message = result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return result;
        }

        private List<string> TextChoiceAnalysis(ReportResponseAnalysis response, QuestionAnalysisCollection surveyResponse)
        {
            List<string> result = new List<string>();

            try
            {
                int sum = surveyResponse.Summary.Values.Sum();
                var ordered = surveyResponse.Summary.OrderBy(x => x.Value);

                result.Add(string.Format("{0} people answered with the least popular response of: {1}", ordered.First().Value.ToString(), ordered.First().Key.ToString()));
                result.Add(string.Format("{0} people answered with the most popular response of: {1}", ordered.Last().Value.ToString(), ordered.Last().Key.ToString()));
                result.Add(string.Format("A total of {0} people participated in this survey.", sum));

                response.Message = result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception Caught - " + ex.Message);
            }

            return result;
        }
    }
}
