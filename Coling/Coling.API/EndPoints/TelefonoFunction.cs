using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Coling.API.Afiliados.EndPoints
{
    public class TelefonoFunction
    {
        private readonly ILogger<TelefonoFunction> _logger;

        public TelefonoFunction(ILogger<TelefonoFunction> logger)
        {
            _logger = logger;
        }

        [Function("TelefonoFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
