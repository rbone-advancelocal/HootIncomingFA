using System;
using System.IO;
using System.Threading.Tasks;
using HootIncomingFA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace HootIncomingFA
{
    public static class ProcessHootMessage
    {
        [FunctionName("ProcessHootMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ExecutionContext context,
            ILogger log)
        {
            log.LogInformation($"Seting connection to Blob accout to use for upload");
            CloudStorageAccount storageAccount = GetCloudStorageAccount(context, log);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("outgoinghoots");

            string incomingHoot = await new StreamReader(req.Body).ReadToEndAsync();
            HootBasics hootie = JsonConvert.DeserializeObject<HootBasics>(incomingHoot);

            try
            {
                hootie.Environment = Environment.GetEnvironmentVariable("Environment", EnvironmentVariableTarget.Process);

                OutputConstructor oc = new OutputConstructor();
                log.LogInformation($"Calling to build the Json for Hoot {hootie.Id}");
                var orderJson = await oc.BuildOutHoot(hootie, log);

                log.LogInformation($"Setting Blob file Name to {hootie.Id}");
                string outName = hootie.Id;

                log.LogInformation($"Container Name = {container.Name}");
                log.LogInformation($"Output Name = {outName}");
                CloudBlockBlob blob = container.GetBlockBlobReference(outName);
                blob.Properties.ContentType = "application/json";

                using (var ms = new MemoryStream())
                {
                    LoadStreamWithJson(ms, orderJson, log);
                    await blob.UploadFromStreamAsync(ms);
                }
                await blob.SetPropertiesAsync();

                return new OkObjectResult("done");
            }
            catch(Exception ex)
            {
                log.LogError($"Error Message = {ex.Message}  Stack Trace = {ex.StackTrace}");
                return new BadRequestObjectResult($"Error Message = {ex.Message}  Stack Trace = {ex.StackTrace}");
            }
        }

        private static CloudStorageAccount GetCloudStorageAccount(ExecutionContext executionContext, ILogger log)
        {
            log.LogInformation($"Starting GetCloudStorageAccount");
            log.LogInformation($"Execution Context Function App Directory = {executionContext.FunctionAppDirectory}");
            var config = new ConfigurationBuilder()
                            .SetBasePath(executionContext.FunctionAppDirectory)
                            //.AddJsonFile("local.settings.json", true, true)
                            .AddEnvironmentVariables().Build();

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config["CloudStorageAccount"]);

            return storageAccount;
        }

        private static void LoadStreamWithJson(Stream ms, object obj, ILogger log)
        {
            log.LogInformation($"Starting LoadStreamWithJson");
            StreamWriter writer = new StreamWriter(ms);
            writer.Write(obj);
            writer.Flush();
            ms.Position = 0;
        }
    }
}

