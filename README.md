# Sample: Copy data from Microsoft MDL to customer's MDL
This sample demonstrates how to create a SAS token programmatically and copy data from Microsoft's MDL to customer MDL covering steps 4 & 5 from: https://community.dynamics.com/365/dynamics-365-fasttrack/b/dynamics-365-fasttrack-blog/posts/custom-interactions-reporting-in-dynamics-marketing

## Running this sample

1. Click Tools -> Nuget Package Manager -> Manage Nuget Packages for Solution and install the following packages:
    - Azure.Identity
    - Azure.Storage.Blobs
    - Azure.Storage.Files.DataLake
    - Microsoft.Identity.Client
    - Newtonsoft.Json
    - System.Configuration.ConfigurationManager
2. Open the *App.config* file and update the connection string with URL for the Dynamics Marketing environment, username and password.
3. Build the project and run the program.
4. Follow the command line prompt to enter the following values:
    - containerURL: https://storageaccountname.dfs.core.windows.net/containername
    - pathURL: path of msdynmkt_aria_exporter_folder
    - destinnation storage account name: customer's storage account name
    - destination blob container name: customer's container name in the storage account provided above
5. When prompted authenticate your storage account via browser.
6. Verify that the files are copied from source to destination.
