using Coling.API.Afiliados.Contratos;
using Coling.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Afiliados.EndPoints
{
    public class PersonaFunction
    {
        private readonly ILogger<PersonaFunction> _logger;
        private readonly IPersonaLogic personaLogic;

        public PersonaFunction(ILogger<PersonaFunction> logger, IPersonaLogic _personaLogic)
        {
            _logger = logger;
            personaLogic = _personaLogic;
        }

        [Function("ListarPersonaFunction")]
        public async Task<HttpResponseData> ListarPersonas([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarPersonas")] HttpRequestData req)
        {
            _logger.LogInformation("ListarPersonas");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(personaLogic.ListarPersonas());
            return res;
        }

        [Function("InsertarPersonaFunction")]
        public async Task<HttpResponseData> InsertarPersona([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarPersona")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarPersona");
            var per = await req.ReadFromJsonAsync<Persona>();
            bool succes = await personaLogic.InsertarPersona(per);
            if (succes)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ModificarPersonaFunction")]
        public async Task<HttpResponseData> ModificarPersona([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ModificarPersona")] HttpRequestData req)
        {
            _logger.LogInformation("ModificarPersona");
            var per = await req.ReadFromJsonAsync<Persona>();
            bool succes = await personaLogic.ModificarPersona(per, per.Id);
            if (succes)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarPersonaFunction")]
        public async Task<HttpResponseData> EliminarPersona([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarPersona")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarPersona");
            var per = await req.ReadFromJsonAsync<Persona>();
            bool succes = await personaLogic.EliminarPersona(per.Id);
            if (succes)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerPersonaFunction")]
        public async Task<HttpResponseData> ObtenerPersona([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerPersonaById")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerPersona");
            var res = req.CreateResponse(HttpStatusCode.OK);
            var per = await req.ReadFromJsonAsync<Persona>();
            await res.WriteAsJsonAsync(personaLogic.ObtenerPersonaById(per.Id));
            return res;
        }
    }
}
