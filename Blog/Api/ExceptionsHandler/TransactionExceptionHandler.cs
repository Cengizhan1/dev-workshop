using Api.Exceptions;

namespace Api.ExceptionsHandler;

public class TransactionExceptionHandler : ExceptionHandlerBase<TransactionException>
{
    public TransactionExceptionHandler(ILogger<TransactionExceptionHandler> logger)
        : base(logger, StatusCodes.Status500InternalServerError, "The transaction could not be completed.") { }
}

