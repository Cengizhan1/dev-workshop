using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.ExceptionsHandler;

public abstract class ExceptionHandlerBase<TException> : IExceptionHandler where TException : Exception
{
    private readonly ILogger _logger;
    private readonly int _statusCode;
    private readonly string _title;

    protected ExceptionHandlerBase(ILogger logger, int statusCode, string title)
    {
        _logger = logger;
        _statusCode = statusCode;
        _title = title;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not TException specificException)
            return false;

        _logger.LogInformation("{ExceptionType} was thrown.", typeof(TException).Name);

        var model = new ProblemDetails
        {
            Status = _statusCode,
            Type = typeof(TException).Name,
            Title = _title,
            Detail = specificException.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
        };

        httpContext.Response.StatusCode = _statusCode;
        await httpContext.Response.WriteAsJsonAsync(model, cancellationToken);

        return true;
    }
}

