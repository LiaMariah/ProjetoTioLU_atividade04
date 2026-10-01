// Lia Mariah Couto Olivo
namespace AcademiaDoZe.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string mensagem) : base(mensagem) { }
    public DomainException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
}
