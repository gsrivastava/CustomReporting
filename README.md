# Sample: Copy data from Microsoft MDL to customer's MDL
This sample demonstrates how to create a SAS token programmatically and copy data from Microsoft's MDL to customer MDL covering steps 4 & 5 from: https://community.dynamics.com/365/dynamics-365-fasttrack/b/dynamics-365-fasttrack-blog/posts/custom-interactions-reporting-in-dynamics-marketing

## 2023-10-23 - The site has been updated and no longer includes the required information. Copied here
> Extracting marketing insights
> As mentioned above, the interaction data collected by Marketing is stored within the Microsoft-managed data lake. It is however possible to extract this data using, the process described here. The article shows how to get to the CDS folder for all data in the Managed Data Lake but for Marketing interactions, we are interested in the msdynmkt_aria_exporter folder. To support this extraction process, the GitHub Project here can be used as a sample code. It should be noted that the code will need to be adapted to the specific needs.
> 
>  Using this project, it is possible to copy the Marketing interaction data from the Managed Data Lake into an Azure Data Container by following this procedure:
> 1. Download the project from GitHub (GitHub - gsrivastava/CustomReporting).
> 2. Identify the URL for the Dynamics Marketing environment and note that as EnvironmentURL.
> 3. Access the https://<EnvironmentURL>/api/data/v9.1/datalakefolders URL
> 4. In the resulting JSON, search for data lake folder with name msdynmkt_aria_exporter_folder and then:
>    1. Locate the Container URL property, indicated by containerendpoint and make note of the value as ContainerURL
>    2. Locate the path property and make a note of the GUID provided as PathURL
> 5. Create an Azure Storage Account and a Blob Container, noting the name for both.
> 6. Provide the “Storage Blob Container” role to the user that will be used to copy the data into the storage account.
> 7. Follow the steps described in the ReadMe file of the downloaded GitHub project to build and run the program which will copy the files from the managed data lake. You will need the ContainerURL, PathUrl , storage account name and blob container name to where files will be copied to
>
> If rather than copying the data all that is required is to verify the content of the managed data lake, you can generate a SAS token to gain access to the files. To do this:
> 1. Use the values from 4a and 4b above to obtain a SAS token from https://<EnvironmentURL>/api/data/v9.1/RetrieveAnalyticsStoreAccess(Url=@a,ResourceType='Folder',Permissions='Read,List')?@a='<ContainerURL>/<PathURL>'
> 2. Use the SAS token to construct a SAS URL as https://<ContainerURL>/<PathURL>?<SASToken> and access through the desired application, like Azure Storage Explorer. The below screenshot shows the access when using the SAS URL to connect to ADLS Gen 2 Container

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
