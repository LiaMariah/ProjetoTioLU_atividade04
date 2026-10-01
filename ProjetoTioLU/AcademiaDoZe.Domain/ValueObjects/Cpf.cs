// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Services;

namespace AcademiaDoZe.Domain.ValueObjects;

public record Cpf
{
    public string Valor { get; }

    private Cpf(string valor)
    {
        Valor = valor;
    }

    public static Result<Cpf> Criar(string valor)
    {
        var textoLimpo = NormalizacaoService.LimparDigitos(valor);

        if (string.IsNullOrWhiteSpace(textoLimpo))
            return Result<Cpf>.Failure("Cpf", "CPF_OBRIGATORIO");

        if (textoLimpo.Length != 11)
            return Result<Cpf>.Failure("Cpf", "CPF_DIGITOS_INVALIDOS");

        if (!ValidarDigitos(textoLimpo))
            return Result<Cpf>.Failure("Cpf", "CPF_INVALIDO");

        return Result<Cpf>.Success(new Cpf(textoLimpo));
    }

    private static bool ValidarDigitos(string cpf)
    {
        // Verifica se todos os dígitos são iguais
        if (cpf.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        var tempCpf = cpf[..9];
        var soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador1[i]).Sum();
        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digito)
            return false;

        tempCpf += digito;
        soma = tempCpf.Select((t, i) => int.Parse(t.ToString()) * multiplicador2[i]).Sum();
        resto = soma % 11;
        digito = resto < 2 ? 0 : 11 - resto;

        return int.Parse(cpf[10].ToString()) == digito;
    }

    public override string ToString() => Valor;
}
