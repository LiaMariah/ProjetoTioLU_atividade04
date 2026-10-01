// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.ValueObjects;

Console.WriteLine("=== AcademiaDoZe.Domain ===");

var cpf = Cpf.Criar("529.982.247-25");
var email = Email.Criar("  ALUNA@EXEMPLO.COM ");
var telefone = Telefone.Criar("(48) 99999-9999");
var cep = Cep.Criar("88.520-000");

ExibirResultado("CPF", cpf);
ExibirResultado("E-mail", email);
ExibirResultado("Telefone", telefone);
ExibirResultado("CEP", cep);

static void ExibirResultado<T>(string nome, AcademiaDoZe.Domain.Common.Result<T> resultado)
{
    if (resultado.IsSuccess)
    {
        Console.WriteLine($"{nome} criado com sucesso: {resultado.Value}");
        return;
    }

    Console.WriteLine($"{nome} inválido:");
    foreach (var notificacao in resultado.Notifications)
        Console.WriteLine($"- {notificacao.Propriedade}: {notificacao.Mensagem}");
}
