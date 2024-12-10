using Api.Exceptions;
using System.Net;

namespace Api.ExceptionsHandler;


public class NotFoundExceptionHandler : ExceptionHandlerBase<NotFoundException>
{
    public NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
        : base(logger, StatusCodes.Status404NotFound, "The requested resource was not found.") { }
}

