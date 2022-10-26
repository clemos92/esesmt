using ESESMT.Infra.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ESESMT.Application.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private IApplicationLogger ApplicationLogger { get; }

        public ExceptionHandlerMiddleware(RequestDelegate next,
            IOptions<MvcNewtonsoftJsonOptions> mvcJsonOptions,
            IApplicationLogger applicationLogger)
        {
            _next = next;
            _jsonSerializerSettings = mvcJsonOptions.Value.SerializerSettings;
            ApplicationLogger = applicationLogger ??
                throw new ArgumentNullException(nameof(applicationLogger));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                ApplicationLogger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Unexpected error ocurred. Contact the system's adminstrator";
            await response.WriteAsync(JsonConvert.SerializeObject(
                new
                {
                    Status = response.StatusCode,
                    Message = message,
                    Exception = exception
                }, 
                _jsonSerializerSettings));
        }
    }
}
