namespace Api.Exceptions;

public class TransactionException : Exception
{
    public TransactionException(string message = "The transaction could not be completed.") : base(message) { }
}
