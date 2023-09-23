using System.Net;
using Azure.Identity;
using Azure.Storage.Queues;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace QueueRequestFunction
{
    public class Function
    {
        private readonly ILogger _logger;

        public Function(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function>();
        }

        [Function("QueueCreateUser")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            try
            {
                var requestBody = string.Empty;
                using (StreamReader streamReader = new StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }

                await QueueRequest(requestBody);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        private async Task QueueRequest(string message)
        {
            var azureStorageConnectionString = Environment.GetEnvironmentVariable("AzureStorageConnectionString");

            // Instantiate a QueueClient to create and interact with the queue
            var queueClient = new QueueClient(azureStorageConnectionString, "createuser");

            await queueClient.CreateAsync();
            await queueClient.SendMessageAsync(message);
        }
    }
}
