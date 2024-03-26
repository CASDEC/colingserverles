using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Coling.Repositorio;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Coling.Autentificacion.Model;
using System.Net;
using Coling.Repositorio.Contratos;
using Microsoft.Azure.Functions.Worker.Http;
using Coling.Repositorio.Implementacion;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;

namespace Coling.Autentificacion
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public Function1(ILogger<Function1> logger, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [Function("Function1")]
        [OpenApiOperation("Accountspec", "Account", Description = "Obten las credenciales si son validas")]
        [OpenApiRequestBody("application/json", typeof(Credenciales), Description ="Introduzaca sus credenciales")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType:typeof(ITokenData), Description = "El Token es")]
        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var credentials = JsonConvert.DeserializeObject<Credenciales>(requestBody);
            bool isValidUser = await _usuarioRepositorio.VerificarCredenciales(credentials.UserName, credentials.Password);
            

            var response = req.CreateResponse();

            if (isValidUser)
            {
                var tokenData = GenerateJwtToken();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteStringAsync(JsonConvert.SerializeObject(tokenData));
            }
            else
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
            }

            return response;
        }

        private static TokenData GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("clave")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, "user_id"),
            new Claim(JwtRegisteredClaimNames.Email, "user@example.com"),
            new Claim("role", "User"),
            };
            var token = new JwtSecurityToken(
                issuer: "yourIssuer",
                audience: "yourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new TokenData
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expire = DateTime.Now.AddMinutes(30)
            };
        }
    }
}
