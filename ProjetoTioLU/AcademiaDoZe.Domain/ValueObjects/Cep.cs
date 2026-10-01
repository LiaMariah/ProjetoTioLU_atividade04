// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Services;

namespace AcademiaDoZe.Domain.ValueObjects;

public record Cep
{
    public string Valor { get; }

    private Cep(string valor)
    {
        Valor = valor;
    }

    public static Result<Cep> Criar(string valor)
    {
        var textoLimpo = NormalizacaoService.LimparDigitos(valor);

        if (string.IsNullOrWhiteSpace(textoLimpo))
            return Result<Cep>.Failure("Cep", "CEP_OBRIGATORIO");

        if (textoLimpo.Length != 8)
            return Result<Cep>.Failure("Cep", "CEP_DIGITOS_INVALIDOS");

        return Result<Cep>.Success(new Cep(textoLimpo));
    }

    public override string ToString() => Valor;
}
