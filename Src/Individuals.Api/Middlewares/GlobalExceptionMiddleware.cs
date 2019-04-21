using System;
using System.Text;
using System.Threading.Tasks;
using Individuals.Shared.ApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;

namespace Individuals.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<GlobalExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Global exception occured in application.");

                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var jsonString = JsonConvert.SerializeObject(ApiResponseHandler.GenerateInternalError());
                await httpContext.Response.WriteAsync(jsonString, Encoding.UTF8);

                return;

            }
        }
    }
}
