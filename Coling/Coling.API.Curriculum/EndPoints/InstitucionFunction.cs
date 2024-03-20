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
    public class InstitucionFunction
    {
        private readonly ILogger<InstitucionFunction> _logger;
        private readonly IRepositorio _repositorio;
        public InstitucionFunction(ILogger<InstitucionFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarInstitucion")]
        public async Task<HttpResponseData> ListarInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ListarInstitucion");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<Institucion>());
            return res;

        }
        [Function("InsertarInstitucion")]
        public async Task<HttpResponseData> InsertarInstitucion([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarInstitucion");
            var data = await req.ReadFromJsonAsync<Institucion>();
            bool success = await _repositorio.Insertar<Institucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarInstitucion")]
        public async Task<HttpResponseData> ActualizarInstitucion([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarInstitucion");
            var data = await req.ReadFromJsonAsync<Institucion>();
            bool success = await _repositorio.Actualizar<Institucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarInstitucion")]
        public async Task<HttpResponseData> EliminarInstitucion([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarInstitucion");
            var data = await req.ReadFromJsonAsync<Institucion>();
            bool success = await _repositorio.Eliminar<Institucion>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerInstitucion")]
        public async Task<HttpResponseData> ObtenerInstitucion([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerInstitucion")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerInstitucion");
            var data = await req.ReadFromJsonAsync<Institucion>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<Institucion>(data));
            return res;
        }
    }
}
