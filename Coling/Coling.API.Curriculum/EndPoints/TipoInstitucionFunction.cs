using Coling.API.Curriculum.Contratos.Repositorios;
using Coling.API.Curriculum.Implementacion.Repositorios;
using Coling.API.Curriculum.Modelo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Coling.API.Curriculum.EndPoints
{
    public class TipoInstitucionFunction
    {
        private readonly ILogger<TipoInstitucionFunction> _logger;
        private readonly IRepositorio _repositorio;

        public TipoInstitucionFunction(ILogger<TipoInstitucionFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarTipoInstitucion")]
        public async Task<HttpResponseData> ListarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarTipoInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ListarTipo");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<TipoInstitucion>());
            return res;

        }
        [Function("InsertarTipoInstitucion")]
        public async Task<HttpResponseData> InsertarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarTipoInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarTipo");
            var data = await req.ReadFromJsonAsync<TipoInstitucion>();
            bool success = await _repositorio.Insertar<TipoInstitucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarTipoInstitucion")]
        public async Task<HttpResponseData> ActualizarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarTipoInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarTipoInstitucion");
            var data = await req.ReadFromJsonAsync<TipoInstitucion>();
            bool success = await _repositorio.Actualizar<TipoInstitucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarTipoInstitucion")]
        public async Task<HttpResponseData> EliminarTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarTipoInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarTipoInstitucion");
            var data = await req.ReadFromJsonAsync<TipoInstitucion>();
            bool success = await _repositorio.Eliminar<TipoInstitucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerTipoInstitucion")]
        public async Task<HttpResponseData> ObtenerTipoInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerTipoInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerTipoInstitucion");
            var data = await req.ReadFromJsonAsync<TipoInstitucion>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<TipoInstitucion>(data));
            return res;
        }

    }
}
