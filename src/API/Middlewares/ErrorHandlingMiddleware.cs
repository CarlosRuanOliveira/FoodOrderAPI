using System.Net;
using System.Text.Json;
using Application.Errors;

namespace API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Erro de requisição inválida");
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, new
                {
                    error = ErrorMsg.InvalidRequest,
                    message = ex.Message
                });
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Recurso não encontrado");
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno no servidor");
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, new
                {
                    message = ErrorMsg.InternalError
                });
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, object responseObj)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(responseObj, options));
        }
    }
}