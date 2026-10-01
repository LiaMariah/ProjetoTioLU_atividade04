// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Services;
using AcademiaDoZe.Domain.ValueObjects;

namespace AcademiaDoZe.Domain.Entities;

public class Aluno : Pessoa, IAggregateRoot
{
    private readonly List<Matricula> _matriculas = [];
    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();

    private Aluno(
        int id,
        string nome,
        Cpf cpf,
        DateOnly dataNascimento,
        Telefone telefone,
        Email email,
        Endereco endereco,
        Senha senha,
        Arquivo? foto = null)
        : base(id, nome, cpf, dataNascimento, telefone, email, endereco, senha, foto)
    {
    }

    public static Result<Aluno> Criar(
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
        Arquivo? foto = null)
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

            if (idade < 12)
                notifications.Add(new Notification("DataNascimento", "DATA_NASCIMENTO_MINIMA_INVALIDA"));
        }

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
            return Result<Aluno>.Failure(notifications);

        var aluno = new Aluno(
            id,
            nome,
            cpfResult.Value!,
            dataNascimento,
            telefoneResult.Value!,
            emailResult.Value!,
            enderecoResult.Value!,
            senhaResult.Value!,
            foto
        );

        return Result<Aluno>.Success(aluno);
    }

    public Result<bool> AdicionarMatricula(Matricula matricula)
    {
        if (matricula is null)
            return Result<bool>.Failure("Matricula", "MATRICULA_OBRIGATORIA");

        if (_matriculas.Any(m => m.Ativa && m.Id != matricula.Id))
            return Result<bool>.Failure("Matricula", "ALUNO_JA_POSSUI_MATRICULA_ATIVA");

        _matriculas.Add(matricula);
        return Result<bool>.Success(true);
    }
}
