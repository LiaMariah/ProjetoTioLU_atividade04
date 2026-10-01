namespace AcademiaDoZe.Infrastructure.Exceptions;

public sealed class InfrastructureException : Exception
{
    public string ErrorCode { get; }

    public InfrastructureException(
        string errorCode,
        string message,
        Exception? innerException = null)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
