using System.Net;
using System.Text.Json;

namespace LibraryManagement.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "An error occurred while processing your request.";
        var errors = new List<string>();

        // Handle specific exception types
        switch (exception)
        {
            case ArgumentNullException argNullEx:
                code = HttpStatusCode.BadRequest;
                message = "Invalid request parameters.";
                errors.Add(argNullEx.Message);
                break;
            case ArgumentException argEx:
                code = HttpStatusCode.BadRequest;
                message = "Invalid request parameters.";
                errors.Add(argEx.Message);
                break;
            case KeyNotFoundException:
            case InvalidOperationException when exception.Message.Contains("not found", StringComparison.OrdinalIgnoreCase):
                code = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case InvalidOperationException invalidOpEx:
                code = HttpStatusCode.BadRequest;
                message = invalidOpEx.Message;
                break;
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                message = "Unauthorized access.";
                break;
            default:
                // Log unexpected exceptions with full details
                var logger = context.RequestServices.GetRequiredService<ILogger<ExceptionHandlingMiddleware>>();
                logger.LogError(exception, "Unhandled exception: {ExceptionType}", exception.GetType().Name);
                break;
        }

        var result = JsonSerializer.Serialize(new
        {
            success = false,
            message,
            errors = errors.Any() ? errors : null,
            statusCode = (int)code,
            timestamp = DateTime.UtcNow
        }, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}

