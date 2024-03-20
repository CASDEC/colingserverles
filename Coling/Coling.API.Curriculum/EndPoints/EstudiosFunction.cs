using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Modelo;
using Coling.Shared.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Coling.API.Curriculum.EndPoints
{
    public class EstudiosFunction
    {
        private readonly ILogger<EstudiosFunction> _logger;
        private readonly IRepositorio _repositorio;
        public EstudiosFunction(ILogger<EstudiosFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarEstudios")]
        public async Task<HttpResponseData> ListarEstudios([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarEstudios")] HttpRequestData req)
        {
            _logger.LogInformation("ListarEstudios");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<Estudios>());
            return res;

        }
        [Function("InsertarEstudios")]
        public async Task<HttpResponseData> InsertarEstudios([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarEstudios")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarEstudios");
            var data = await req.ReadFromJsonAsync<Estudios>();
            bool success = await _repositorio.Insertar<Estudios>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarEstudios")]
        public async Task<HttpResponseData> ActualizarEstudios([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarEstudios")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarEstudios");
            var data = await req.ReadFromJsonAsync<Estudios>();
            bool success = await _repositorio.Actualizar<Estudios>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarEstudios")]
        public async Task<HttpResponseData> EliminarEstudios([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarEstudios")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarEstudios");
            var data = await req.ReadFromJsonAsync<Estudios>();
            bool success = await _repositorio.Eliminar<Estudios>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerEstudios")]
        public async Task<HttpResponseData> ObtenerEstudios([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerEstudios")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerEstudios");
            var data = await req.ReadFromJsonAsync<Estudios>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<Estudios>(data));
            return res;
        }
    }
}
