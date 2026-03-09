namespace Store.Host;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
    {
        { typeof(ArgumentException), StatusCodes.Status400BadRequest },
        { typeof(KeyNotFoundException), StatusCodes.Status404NotFound },
        { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
        { typeof(InvalidOperationException), StatusCodes.Status409Conflict }
    };

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            _logger.LogError(ex, "Erro não tratado");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = StatusCodes.Status500InternalServerError;

        foreach (var entry in ExceptionStatusCodes)
        {
            if (entry.Key.IsAssignableFrom(exception.GetType()))
            {
                statusCode = entry.Value;
                break;
            }
        }

        var message = statusCode == StatusCodes.Status500InternalServerError
            ? "Erro interno no servidor"
            : exception.Message;

        var response = new
        {
            status = statusCode,
            message = message,
            traceId = context.TraceIdentifier
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
}
