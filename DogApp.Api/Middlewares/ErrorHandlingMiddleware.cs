using System.Net;
using System.Data.Common;
using DogApp.Application.Models;
using Newtonsoft.Json;

namespace DogApp.Api.Middlewares
{
    /// <summary>
    /// This middleware is used for unexpected erros. Do not use it for manually exceptions throwing
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                await HandleException(error, context, env.IsDevelopment());
            }
        }

        private async Task HandleException(Exception error, HttpContext context, bool isDevelopment)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetCode(error);

            var errorMessage = GetErrorMessage(error, isDevelopment, context.Response.StatusCode);
            var errorResponse = new ErrorResponse { Message= errorMessage };         
            var responseBody = JsonConvert.SerializeObject(errorResponse);

            await context.Response.WriteAsync(responseBody);
        }

        private static int GetCode(Exception error)
        {
            return error switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                DbException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };
        }

        private static string GetErrorMessage(Exception ex, bool isDevelopment, int httpStatusCode)
        {
            string errorMessage;
            if (isDevelopment)
                errorMessage = ex.ToString();
            else if (httpStatusCode >= (int)HttpStatusCode.InternalServerError)
                errorMessage = "Something wrong Happened";
            else
                errorMessage = ex.Message;

            return errorMessage;
        }
    }
}
