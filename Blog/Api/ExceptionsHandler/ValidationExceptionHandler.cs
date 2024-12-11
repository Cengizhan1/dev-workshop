using FluentValidation;

namespace Api.ExceptionsHandler;

public class ValidationExceptionHandler : ExceptionHandlerBase<ValidationException>
{
    public ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
        : base(logger, StatusCodes.Status400BadRequest, "Validation error occurred.") { }
}

