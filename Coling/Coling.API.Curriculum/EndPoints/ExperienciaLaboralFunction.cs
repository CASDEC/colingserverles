using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class ExperienciaLaboralFunction
    {
        private readonly ILogger<ExperienciaLaboralFunction> _logger;
        private readonly IRepositorio _repositorio;
        public ExperienciaLaboralFunction(ILogger<ExperienciaLaboralFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarExperienciaLaboral")]
        public async Task<HttpResponseData> ListarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarExperienciaLaboral")] HttpRequestData req)
        {
            _logger.LogInformation("ListarExperienciaLaboral");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<ExperienciaLaboral>());
            return res;

        }
        [Function("InsertarExperienciaLaboral")]
        public async Task<HttpResponseData> InsertarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarExperienciaLaboral")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarExperienciaLaboral");
            var data = await req.ReadFromJsonAsync<ExperienciaLaboral>();
            bool success = await _repositorio.Insertar<ExperienciaLaboral>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarExperienciaLaboral")]
        public async Task<HttpResponseData> ActualizarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarExperienciaLaboral")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizaActualizarExperienciaLaboralrEstudios");
            var data = await req.ReadFromJsonAsync<ExperienciaLaboral>();
            bool success = await _repositorio.Actualizar<ExperienciaLaboral>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarExperienciaLaboral")]
        public async Task<HttpResponseData> EliminarExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarExperienciaLaboral")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarExperienciaLaboral");
            var data = await req.ReadFromJsonAsync<ExperienciaLaboral>();
            bool success = await _repositorio.Eliminar<ExperienciaLaboral>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerExperienciaLaboral")]
        public async Task<HttpResponseData> ObtenerExperienciaLaboral([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerExperienciaLaboral")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerExperienciaLaboral");
            var data = await req.ReadFromJsonAsync<ExperienciaLaboral>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<ExperienciaLaboral>(data));
            return res;
        }
    }
}
