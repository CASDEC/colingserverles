using Coling.API.Afiliados.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliados.EndPoints
{
    public class AfiliadoFunction
    {
        private readonly ILogger<AfiliadoFunction> _logger;
        private readonly IAfiliadoLogic afiliadoLogic;

        public AfiliadoFunction(ILogger<AfiliadoFunction> logger, IAfiliadoLogic _afiliadoLogic)
        {
            _logger = logger;
            afiliadoLogic = _afiliadoLogic;
        }

        [Function("AfiliadoFunction")]
        public async Task<HttpResponseData> ListarAfiliados([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarAfiliados")] HttpRequestData req)
        {
            _logger.LogInformation("ListarAfiliados");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(afiliadoLogic.ListarAfiliados());
            return res;
        }
    }
}
