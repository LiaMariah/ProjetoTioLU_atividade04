// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.Services;
using AcademiaDoZe.Domain.ValueObjects;

namespace AcademiaDoZe.Domain.Entities;

public class Colaborador : Pessoa, IAggregateRoot
{
    public DateOnly DataAdmissao { get; private set; }
    public ColaboradorTipo Tipo { get; private set; }
    public ColaboradorVinculo Vinculo { get; private set; }

    private Colaborador(
        int id,
        string nome,
        Cpf cpf,
        DateOnly dataNascimento,
        Telefone telefone,
        Email email,
        Endereco endereco,
        Senha senha,
        Arquivo? foto,
        DateOnly dataAdmissao,
        ColaboradorTipo tipo,
        ColaboradorVinculo vinculo)
        : base(id, nome, cpf, dataNascimento, telefone, email, endereco, senha, foto)
    {
        DataAdmissao = dataAdmissao;
        Tipo = tipo;
        Vinculo = vinculo;
    }

    public static Result<Colaborador> Criar(
        int id,
        string nome,
        string cpf,
        DateOnly dataNascimento,
        string telefone,
        string email,
        Logradouro logradouro,
        string numero,
        string complemento,
        string senha,
        Arquivo? foto,
        DateOnly dataAdmissao,
        ColaboradorTipo tipo,
        ColaboradorVinculo vinculo)
    {
        var notifications = new List<Notification>();

        // Validação do Nome
        if (NormalizacaoService.TextoVazioOuNulo(nome))
            notifications.Add(new Notification("Nome", "NOME_OBRIGATORIO"));
        else
            nome = NormalizacaoService.LimparEspacos(nome);

        // Validação da Data de Nascimento
        if (dataNascimento == default)
            notifications.Add(new Notification("DataNascimento", "DATA_NASCIMENTO_OBRIGATORIO"));
        else
        {
            var idade = DateOnly.FromDateTime(DateTime.Today).Year - dataNascimento.Year;
            if (dataNascimento > DateOnly.FromDateTime(DateTime.Today.AddYears(-idade)))
                idade--;

            if (idade < 18)
                notifications.Add(new Notification("DataNascimento", "DATA_NASCIMENTO_MINIMA_INVALIDA"));
        }

        // Validação da Data de Admissão
        if (dataAdmissao == default)
            notifications.Add(new Notification("DataAdmissao", "DATA_ADMISSAO_OBRIGATORIO"));
        else if (dataAdmissao > DateOnly.FromDateTime(DateTime.Today))
            notifications.Add(new Notification("DataAdmissao", "DATA_ADMISSAO_MAIOR_ATUAL"));

        // Validação do Tipo
        if (!Enum.IsDefined(tipo))
            notifications.Add(new Notification("Tipo", "TIPO_COLABORADOR_INVALIDO"));

        // Validação do Vínculo
        if (!Enum.IsDefined(vinculo))
            notifications.Add(new Notification("Vinculo", "VINCULO_COLABORADOR_INVALIDO"));

        // Regra: Administrador deve ser CLT
        if (Enum.IsDefined(tipo) && Enum.IsDefined(vinculo) &&
            tipo == ColaboradorTipo.Administrador && vinculo != ColaboradorVinculo.CLT)
            notifications.Add(new Notification("Vinculo", "ADMINISTRADOR_DEVE_SER_CLT"));

        // Instanciação e validação dos ValueObjects
        var cpfResult = Cpf.Criar(cpf);
        if (cpfResult.IsFailure)
            notifications.AddRange(cpfResult.Notifications);

        var telefoneResult = Telefone.Criar(telefone);
        if (telefoneResult.IsFailure)
            notifications.AddRange(telefoneResult.Notifications);

        var emailResult = Email.Criar(email);
        if (emailResult.IsFailure)
            notifications.AddRange(emailResult.Notifications);

        var senhaResult = Senha.Criar(senha);
        if (senhaResult.IsFailure)
            notifications.AddRange(senhaResult.Notifications);

        var enderecoResult = Endereco.Criar(logradouro, numero, complemento);
        if (enderecoResult.IsFailure)
            notifications.AddRange(enderecoResult.Notifications);

        if (notifications.Count != 0)
            return Result<Colaborador>.Failure(notifications);

        var colaborador = new Colaborador(
            id,
            nome,
            cpfResult.Value!,
            dataNascimento,
            telefoneResult.Value!,
            emailResult.Value!,
            enderecoResult.Value!,
            senhaResult.Value!,
            foto,
            dataAdmissao,
            tipo,
            vinculo
        );

        return Result<Colaborador>.Success(colaborador);
    }
}
