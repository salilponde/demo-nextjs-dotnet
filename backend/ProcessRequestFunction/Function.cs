using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebApi.Models.Users;
using WebApi.Services;

namespace ProcessRequestFunction
{
    public class Function
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public Function(ILoggerFactory loggerFactory, IUserService userService)
        {
            _logger = loggerFactory.CreateLogger<Function>();
            _userService = userService;
        }

        [Function("ProcessCreateUser")]
        public async Task Run([QueueTrigger("createuser", Connection = "LocalAzurite")] string createRequestJson)
        {
            try
            {
                var createRequest = JsonConvert.DeserializeObject<CreateRequest>(createRequestJson);
                await _userService.Create(createRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }
    }
}
