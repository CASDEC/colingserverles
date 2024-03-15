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
    public class ProfesionFunction
    {
        private readonly ILogger<ProfesionFunction> _logger;
        private readonly IRepositorio _repositorio;

        public ProfesionFunction(ILogger<ProfesionFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarProfesion")]
        public async Task<HttpResponseData> ListarProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarProfesion")] HttpRequestData req)
        {
            _logger.LogInformation("ListarProfesion");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<Profesion>());
            return res;

        }
        [Function("InsertarProfesion")]
        public async Task<HttpResponseData> InsertarProfesion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarProfesion")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarProfesion");
            var data = await req.ReadFromJsonAsync<Profesion>();
            bool success = await _repositorio.Insertar<Profesion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarProfesion")]
        public async Task<HttpResponseData> ActualizarProfesion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarProfesion")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarProfesion");
            var data = await req.ReadFromJsonAsync<Profesion>();
            bool success = await _repositorio.Actualizar<Profesion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarProfesion")]
        public async Task<HttpResponseData> EliminarProfesion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarProfesion")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarProfesion");
            var data = await req.ReadFromJsonAsync<Profesion>();
            bool success = await _repositorio.Eliminar<Profesion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerProfesion")]
        public async Task<HttpResponseData> ObtenerProfesion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerProfesion")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerProfesion");
            var data = await req.ReadFromJsonAsync<Profesion>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<Profesion>(data));
            return res;
        }
    }
}
