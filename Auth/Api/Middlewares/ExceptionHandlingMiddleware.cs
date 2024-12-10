using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IEnumerable<IExceptionHandler> _handlers;

    public ExceptionHandlingMiddleware(RequestDelegate next, IEnumerable<IExceptionHandler> handlers)
    {
        _next = next;
        _handlers = handlers;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            foreach (var handler in _handlers)
            {
                if (await handler.TryHandleAsync(context, ex, CancellationToken.None))
                    return;
            }

            // Eğer hiç handler yoksa genel bir cevap ver
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = StatusCodes.Status500InternalServerError,
                Message = "An unexpected error occurred."
            });
        }
    }
}
