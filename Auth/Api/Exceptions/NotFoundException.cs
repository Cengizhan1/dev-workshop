namespace Api.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "The requested resource was not found.") : base(message) { }
}
