using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Coling.API.Afiliados.EndPoints
{
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;

        public PersonaFunction(ILogger<PersonaFunction> logger)
        {
            _logger = logger;
        }

        [Function("PersonaFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
