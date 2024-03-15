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
    public class GradoAcademicoFunction
    {
        private readonly ILogger<GradoAcademicoFunction> _logger;
        private readonly IRepositorio _repositorio;
        public GradoAcademicoFunction(ILogger<GradoAcademicoFunction> logger, IRepositorio repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [Function("ListarGradoAcademico")]
        public async Task<HttpResponseData> ListarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ListarGradoAcademico")] HttpRequestData req)
        {
            _logger.LogInformation("ListarGradoAcademico");
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarTodos<GradoAcademico>());
            return res;

        }
        [Function("InsertarGradoAcademico")]
        public async Task<HttpResponseData> InsertarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "post", Route = "InsertarGradoAcademico")] HttpRequestData req)
        {
            _logger.LogInformation("InsertarGradoAcademico");
            var data = await req.ReadFromJsonAsync<GradoAcademico>();
            bool success = await _repositorio.Insertar<GradoAcademico>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("ActualizarGradoAcademico")]
        public async Task<HttpResponseData> ActualizarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "put", Route = "ActualizarGradoAcademico")] HttpRequestData req)
        {
            _logger.LogInformation("ActualizarGradoAcademico");
            var data = await req.ReadFromJsonAsync<GradoAcademico>();
            bool success = await _repositorio.Actualizar<GradoAcademico>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }
        [Function("EliminarGradoAcademico")]
        public async Task<HttpResponseData> EliminarGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "EliminarGradoAcademico")] HttpRequestData req)
        {
            _logger.LogInformation("EliminarGradoAcademico");
            var data = await req.ReadFromJsonAsync<GradoAcademico>();
            bool success = await _repositorio.Eliminar<GradoAcademico>(data);
            if (success)
            {
                var respuesta = req.CreateResponse(HttpStatusCode.OK);
                return respuesta;
            }
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        [Function("ObtenerGradoAcademico")]
        public async Task<HttpResponseData> ObtenerGradoAcademico([HttpTrigger(AuthorizationLevel.Function, "get", Route = "ObtenerGradoAcademico")] HttpRequestData req)
        {
            _logger.LogInformation("ObtenerGradoAcademico");
            var data = await req.ReadFromJsonAsync<GradoAcademico>();
            var res = req.CreateResponse(HttpStatusCode.OK);
            await res.WriteAsJsonAsync(_repositorio.ListarUno<GradoAcademico>(data));
            return res;
        }
    }
}
