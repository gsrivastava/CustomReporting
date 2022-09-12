using Azure.Identity;
using Azure.Storage.Blobs;
using System.Configuration;

namespace CustomReporting
{
    internal class Program
    {
        private static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.Write("Identify the URL for the Dynamics Marketing environment and note that as EnvironmentURL" +
                "\r\nAccess the https://<EnvironmentURL>/api/data/v9.1/datalakefolders URL\r\n" +
                "In the resulting JSON, search for data lake folder with name msdynmkt_aria_exporter_folder " +
                "and then:\r\nLocate the Container URL property, indicated by containerendpoint " +
                "and make note of the value as source ContainerURL: https://<storageaccountname>.dfs.core.windows.net/<containername>\r\n" +
                "Locate the path property and make a note of the GUID provided as source PathURL\r\n");

            Console.WriteLine("Enter containerURL - https://<storageaccountname>.dfs.core.windows.net/<containername>:");
            string sourceContainerUri = Console.ReadLine();

            Console.WriteLine("Enter pathURL - path of msdynmkt_aria_exporter_folder:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Enter destinnation storage account name - samplestorageaccountname:");
            string destStorageAccountName = Console.ReadLine();

            Console.WriteLine("Enter destination blob container name - samplecontainername:");
            string destBlobContainerName = Console.ReadLine();

            string containerPathURL = string.Concat(sourceContainerUri, "/", sourcePath, "/");
            Console.Write("Source container path url is:" + containerPathURL);

            // Construct requestUri to call dataverse API
            string requestUri =
                string.Format("RetrieveAnalyticsStoreAccess(Url=@a,ResourceType='Folder',Permissions='Read,Write,List')?@a='{0}'", containerPathURL);

            //Get configuration data from App.config connectionStrings
            string connectionString = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

            // Get SAS token to list all blobs in the given directory by making a REST api call to:  GET: https://<<org URL>>/api/data/v9.1/RetrieveAnalyticsStoreAccess(Url=@a,ResourceType='File/Folder',Permissions='Read,Write')?@a='file/folder Path'
            client = HttpClientHelpers.GetHttpClient(connectionString, HttpClientHelpers.clientId, HttpClientHelpers.redirectUrl);
            string sasToken = await DatalakeAPIHelper.GetSasTokenAsync(client, requestUri);

            // List all the blobs under source directory
            List<string> blobPaths = DatalakeAPIHelper.ListBlobsWithSas(containerPathURL, sasToken);

            // Construct the blob endpoint from the account name.
            var destStorageAccountString = string.Format("https://{0}.blob.core.windows.net", destStorageAccountName);
            var destinationCredentials = new InteractiveBrowserCredential();
            BlobServiceClient destBlobServiceClient = new BlobServiceClient(new Uri(destStorageAccountString), destinationCredentials);
            BlobContainerClient destContainerClient = destBlobServiceClient.GetBlobContainerClient(destBlobContainerName);

            // Copy each blob from source to destination
            await DatalakeAPIHelper.CopyBlobAsync(client, sourceContainerUri.Replace("dfs", "blob"), blobPaths, destContainerClient);
        }
    }
}