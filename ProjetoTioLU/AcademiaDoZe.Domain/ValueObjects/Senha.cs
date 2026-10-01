// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Services;

namespace AcademiaDoZe.Domain.ValueObjects;

public record Senha
{
    public string Valor { get; }

    private Senha(string valor)
    {
        Valor = valor;
    }

    public static Result<Senha> Criar(string valor)
    {
        var textoLimpo = NormalizacaoService.LimparEspacos(valor);

        if (string.IsNullOrWhiteSpace(textoLimpo))
            return Result<Senha>.Failure("Senha", "SENHA_OBRIGATORIO");

        if (textoLimpo.Length < 6)
            return Result<Senha>.Failure("Senha", "SENHA_MINIMO_CARACTERES");

        return Result<Senha>.Success(new Senha(textoLimpo));
    }

    public override string ToString() => Valor;
}
