using Api.ExceptionsHandler;

namespace Api.Extensions;

public static class ExceptionHandlerExtension
{
    public static IServiceCollection AddCustomExceptionHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<TransactionExceptionHandler>();
        return services;
    }
}
