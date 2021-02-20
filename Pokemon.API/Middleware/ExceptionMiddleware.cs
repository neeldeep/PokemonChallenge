using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pokemon.Entities;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pokemon.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// This is a Customer Middleware that handles Exception across the Application
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var respMsg = code.ToString();

            switch (ex)
            {
                case APIException pokemonAPIError:
                    code = pokemonAPIError.ErrorCode;
                    respMsg = ex.Message;
                    break;
                default:
                    _logger.LogError(ex, "Generic exception");
                    break;
            }

            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(respMsg);
        }

    }
}
