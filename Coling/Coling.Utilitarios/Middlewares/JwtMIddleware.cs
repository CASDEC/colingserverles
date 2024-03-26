using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Utilitarios.Middlewares
{
    public class JwtMIddleware : IFunctionsWorkerMiddleware
    {
        private readonly IConfiguration _configuration;
        public JwtMIddleware(IConfiguration configuration) {
            _configuration = configuration;
        }
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            HttpRequestData request = await context.GetHttpRequestDataAsync();
            if (request != null)
            {
                if (request.Headers.TryGetValues("Authorization", out var authHeader) && authHeader.FirstOrDefault() is { } bearerToken)
                {
                    var token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;

                    var principal = ValidateToken(token);
                    if (principal == null)
                    {
                        HttpResponseData response = request.CreateResponse(HttpStatusCode.Unauthorized);
                        await response.WriteStringAsync("Invalid token");
                        return;
                    }
                }
            }
            await next(context);
        }


        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["clave"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
