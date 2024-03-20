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
    public class TipoEstudioFunction
    {
        private readonly ILogger<TipoEstudioFunction> _logger;
        private readonly IRepositorio _repositorio;

        public TipoEstudioFunction(ILogger<TipoEstudioFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarTipoEstudio")]
        public async Task<HttpResponseData> ListarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoEstudio")] HttpRequestData req)
        {
            _logger.LogInformation("ListarTipoEstudio");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<TipoEstudio>());
            return res;

        }
        [Function("InsertarTipoEstudio")]
        public async Task<HttpResponseData> InsertarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTipoEstudio")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarTipoEstudio");
            var data = await req.ReadFromJsonAsync<TipoEstudio>();
            bool success = await _repositorio.Insertar<TipoEstudio>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarTipoEstudio")]
        public async Task<HttpResponseData> ActualizarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarTipoEstudio")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarTipoEstudio");
            var data = await req.ReadFromJsonAsync<TipoEstudio>();
            bool success = await _repositorio.Actualizar<TipoEstudio>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarTipoEstudio")]
        public async Task<HttpResponseData> EliminarTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTipoEstudio")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarTipoEstudio");
            var data = await req.ReadFromJsonAsync<TipoEstudio>();
            bool success = await _repositorio.Eliminar<TipoEstudio>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerTipoEstudio")]
        public async Task<HttpResponseData> ObtenerTipoEstudio([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTipoEstudio")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerTipoEstudio");
            var data = await req.ReadFromJsonAsync<TipoEstudio>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<TipoEstudio>(data));
            return res;
        }
    }
}
