using WebApp.Business.Interfaces;
using WebApp.Common.Exceptions;

namespace WebApp.Web.Middlewares;

/// <summary>
/// Middleware to handle global exceptions in a structured and extensible way.
/// </summary>
public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IServiceProvider serviceProvider)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ha ocurrido un error inesperado: {Message}", ex.Message);
            
            using (var scope = _serviceProvider.CreateScope())
            {
                var loggerException = scope.ServiceProvider.GetRequiredService<ILoggerException>();
                await loggerException.CreateLogErrorAsync(
                    message: "Ha ocurrido un error inesperado.",
                    exception: ex.ToString(),
                    details: $"{context.Request.Path} {context.Request.QueryString}"
                );
            }

            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorMessage) = GetExceptionDetails(exception);

        var errorPath = "/Home/Error";

        context.Session?.SetString("ErrorMessage", errorMessage);
        context.Session?.SetInt32("ErrorStatusCode", statusCode);
        context.Response.Redirect(errorPath);
        await context.Response.CompleteAsync();
    }

    private (int StatusCode, string Message) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            InvalidOperationException ex => (
                StatusCodes.Status400BadRequest,
                "La operación solicitada no es válida. Verifique los parámetros de su solicitud o contacte al soporte si el problema persiste."
            ),
            UnauthorizedAccessException ex => (
                StatusCodes.Status403Forbidden,
                "No tiene permisos suficientes para acceder a este recurso. Contacte al administrador si necesita acceso."
            ),
            NotFoundException ex => (
                StatusCodes.Status404NotFound,
                "El recurso solicitado no existe o no se encontró. Verifique la URL o el identificador proporcionado."
            ),
            BadRequestException ex => (
                StatusCodes.Status400BadRequest,
                "La solicitud contiene datos inválidos. Revise los parámetros enviados y corríjalos."
            ),
            FluentValidation.ValidationException validationEx => (
                StatusCodes.Status400BadRequest,
                "La validación de la solicitud falló. Por favor, revise los detalles para más información."
            ),
            Exception ex => (
                StatusCodes.Status500InternalServerError,
                "Ha ocurrido un error inesperado en el servidor. Por favor, intente nuevamente más tarde o contacte al soporte técnico."
            )
        };
    }
}