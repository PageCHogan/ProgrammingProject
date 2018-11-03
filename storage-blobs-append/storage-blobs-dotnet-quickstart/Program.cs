//------------------------------------------------------------------------------
// Test appendblob. success, append text existing blobs
// We can keep adding texts at the end
// Example blobs operation. 
// 
// when downloading, download path should be existed beforehand.c:\download\
// INPUT validation has not been implemented. Only main part.
//------------------------------------------------------------------------------


namespace storage_blobs_dotnet_quickstart
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <reference> 
    /// https://www.pmichaels.net/2017/10/15/adding-existing-azure-blob/
    /// https://docs.microsoft.com/en-us/azure/visual-studio/vs-storage-aspnet5-getting-started-blobs
    /// https://docs.microsoft.com/en-au/azure/storage/blobs/storage-quickstart-blobs-dotnet?tabs=windows
    /// </reference>

    public static class Program
    {

        private static string accountName = "sebenv2";
        private static string key = "+hK/4kPTevqxL+OqG60wEIaNjquLfxIXOBkyNkwXJ4g5CIEFUDNnD+2V6PW63omw5EZom6jhBLOTpwwESDd2sQ==";

        //accessing with the name and key
        private static CloudStorageAccount storage = new CloudStorageAccount(
        new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
        accountName, key), true);

        private static CloudBlobClient client = storage.CreateCloudBlobClient();
        //Container name
        private static CloudBlobContainer container = client.GetContainerReference("response-csv");

        public static void Main()
        {
            createIFNotExist().GetAwaiter().GetResult();

            string option;
            do
            {

                Console.WriteLine("\n*Basic operation*");
                Console.WriteLine("List blobs[L]\n"
                      + "Uploading test text[U]\n"
                      + "Download from blobs[D]\n"
                      + "Download blob as text(String)[DT]\n"
                      + "Remove blob(R)\n"
                      + "Appending GP Response[AG]\n" +
                      "Program exit(X)\n");

                option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "L":
                        listblobs().GetAwaiter().GetResult();
                        break;
                    case "U":
                        UploadNewText();
                        break;
                    case "D":
                        downloadblobs().GetAwaiter().GetResult();
                        break;
                    case "DT":
                        downloadAsText().GetAwaiter().GetResult();//Useful when downloading blob as String to API 
                        break;
                    case "R":
                        deleteBlobs().GetAwaiter().GetResult();
                        break;
                    case "AG":
                        appendNewTextGP();
                        break;
                    case "X":
                        break;
                }

            } while (option.Equals("X") == false);

            Console.WriteLine("Bye Bye");
            Console.ReadLine();
        }

        public static void UploadNewText()
        {
            Console.Write("Please enter text to add to the blob: ");
            string text = Console.ReadLine();

            //file name
            string fileName = "test.csv";
            CloudAppendBlob blob = container.GetAppendBlobReference(fileName);

            //Create new blob file, if it does not exist.
            if (blob.ExistsAsync().Result == false)
            {
                blob.CreateOrReplaceAsync();
            }

            //Append text to the end of data.
            blob.AppendTextAsync(text);
        }

        public static void appendNewTextGP()
        {
            //Saving string as responses // 
            /*
               Here I found a problem, without reading previous RESPNUM(respondant number) and REF(ID), 
               I cannot add next RESPNUM and REF.
               
               Solution 1,
               in a database, We need to add the number of responses,
               so we can keep track of numbers when we add to csv.

               Solution 2,
               Making a log file

               Solution 3,
               Do not keep ID and numbers

               Solution 4,
               Download a file, load into a memory and add ID.

               Having discussed, we remove RESPNUM and REF(instead of REF, SurveyID is added)
            */

            string response = "1," + DateTime.Now.ToString("yyyyMMdd") + "," + DateTime.Now.ToString("hh:mm:ss")
                + "," + "1,2,2,1,2,1,5,1,7,5,1,5,4,4,1,2,2,2,2,2,4,3,3,32514, good" + System.Environment.NewLine;

            //file name
            string fileName = "GP_test.csv";
            CloudAppendBlob blob = container.GetAppendBlobReference(fileName);

            //Create new blob file, if it does not exist.
            if (blob.ExistsAsync().Result == false)
            {
                blob.CreateOrReplaceAsync();
            }
            //Append responses to the end of data.
            blob.AppendTextAsync(response);
        }

        private static async Task listblobs()
        {
            // List the blobs in the container.
            Console.WriteLine("List blobs in container.");
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                BlobResultSegment results = await container.ListBlobsSegmentedAsync(null, blobContinuationToken);
                // Get the value of the continuation token returned by the listing call.
                blobContinuationToken = results.ContinuationToken;
                foreach (IListBlobItem item in results.Results)
                {
                    Console.WriteLine(item.Uri);
                }
            } while (blobContinuationToken != null); // Loop while the continuation token is not null.

        }

        private static async Task downloadblobs()
        {

            Console.Write("Please enter text to download from blob: ");
            string text = Console.ReadLine();

            // Get a reference to a blob named.
            CloudAppendBlob appendBlob = container.GetAppendBlobReference(text);

            // Save the blob contents to a file. Path should be made beforehand.
            using (var fileStream = System.IO.File.OpenWrite(@"c:\download\" + text))
            {
                await appendBlob.DownloadToStreamAsync(fileStream);
            }

        }

        private static async Task downloadAsText() // Suggested FOR API DOWNLOADING AS TEXT.
        {
           Console.Write("Please enter text to download from blob: ");
            string text = Console.ReadLine();

           // Get a reference to a blob named.
           CloudAppendBlob appendBlob = container.GetAppendBlobReference(text);

           //Blob donwload as String
           string csvData = await appendBlob.DownloadTextAsync();
           
           //print out the data
           Console.WriteLine(csvData);
        }

        private static async Task deleteBlobs()
        {
            Console.Write("Please enter text to delete from blob: ");
            string text = Console.ReadLine();

            // Get a reference to a blob.
            CloudAppendBlob appendBlob = container.GetAppendBlobReference(text);

            // Delete the blob.
            await appendBlob.DeleteAsync();
        }

        private static async Task createIFNotExist()
        {
            // If "mycontainer" doesn't exist, create it.
            await container.CreateIfNotExistsAsync();
        }



    }
}
