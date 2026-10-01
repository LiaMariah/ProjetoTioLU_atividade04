using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.ValueObjects;
using AcademiaDoZe.Infrastructure.Data;
using AcademiaDoZe.Infrastructure.Repositories;

namespace AcademiaDoZe.Infrastructure.Tests;

public sealed class AlunoRepositoryTests : TestBase
{
    private readonly AlunoRepository _repository;

    public AlunoRepositoryTests()
    {
        _repository = new AlunoRepository(ConnectionString, DatabaseType);
    }

    private async Task<Aluno> CriarAluno()
    {
        await using var logradouroRepository = new LogradouroRepository(ConnectionString, DatabaseType);
        return await CriarAluno(logradouroRepository);
    }

    public static async Task<Aluno> CriarEInserirAlunoAsync(AlunoRepository alunoRepository, LogradouroRepository logradouroRepository)
    {
        var aluno = await CriarAluno(logradouroRepository);
        return await alunoRepository.Adicionar(aluno);
    }

    private static async Task<Aluno> CriarAluno(LogradouroRepository logradouroRepository)
    {
        var resultadoLogradouro = Logradouro.Criar(
            0,
            GerarCep(),
            "Lia Mariah",
            "Olivo",
            "SQLServer",
            "SC",
            "Brasil");

        Assert.True(resultadoLogradouro.IsSuccess, string.Join("; ", resultadoLogradouro.Notifications));

        var logradouro = await logradouroRepository.Adicionar(resultadoLogradouro.Value!);

        var resultado = Aluno.Criar(
            0,
            "Lia Mariah",
            GerarCpf(),
            new DateOnly(2000, 1, 1),
            "48999999999",
            GerarEmail(),
            logradouro,
            "10",
            "Casa",
            "senha123",
            null);

        Assert.True(resultado.IsSuccess, string.Join("; ", resultado.Notifications));
        return resultado.Value!;
    }

    [Fact]
    public async Task Aluno_Adicionar_Sucesso()
    {
        var aluno = await _repository.Adicionar(await CriarAluno());
        Assert.True(aluno.Id > 0);
        Assert.Equal("Lia Mariah", aluno.Nome);

        var obtido = await _repository.ObterPorId(aluno.Id);
        Assert.NotNull(obtido);
        Assert.Equal(aluno.Cpf, obtido!.Cpf);
        Assert.Equal(aluno.Endereco.Logradouro.Id, obtido.Endereco.Logradouro.Id);
    }

    [Fact]
    public async Task Aluno_ObterPorId_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
        Assert.Equal("Lia Mariah", obtido.Nome);
    }

    [Fact]
    public async Task Aluno_ObterTodos_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var todos = await _repository.ObterTodos();
        Assert.NotEmpty(todos);
        Assert.Contains(todos, x => x.Id == inserido.Id);
    }

    [Fact]
    public async Task Aluno_Atualizar_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var atualizado = Aluno.Criar(
            inserido.Id,
            "Lia Atualizada",
            inserido.Cpf.Valor,
            inserido.DataNascimento,
            "48988888888",
            inserido.Email.Valor,
            inserido.Endereco.Logradouro,
            "20",
            "Apartamento",
            inserido.Senha.Valor,
            Arquivo.Criar(new byte[] { 1, 2, 3 }).Value!);

        Assert.True(atualizado.IsSuccess, string.Join("; ", atualizado.Notifications));
        var resultado = await _repository.Atualizar(atualizado.Value!);
        Assert.Equal("Lia Atualizada", resultado.Nome);

        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal("Lia Atualizada", obtido!.Nome);
        Assert.Equal("48988888888", obtido.Telefone.Valor);
        Assert.Equal("20", obtido.Endereco.Numero);
        Assert.Equal("Apartamento", obtido.Endereco.Complemento);
        Assert.NotNull(obtido.Foto);
        Assert.Equal(new byte[] { 1, 2, 3 }, obtido.Foto!.Conteudo);
    }

    [Fact]
    public async Task Aluno_ObterPorCpf_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var obtido = await _repository.ObterPorCpf(inserido.Cpf);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
    }

    [Fact]
    public async Task Aluno_ObterPorEmail_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var obtido = await _repository.ObterPorEmail(inserido.Email);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
    }

    [Fact]
    public async Task Aluno_CpfJaExiste_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        Assert.True(await _repository.CpfJaExiste(inserido.Cpf));
        Assert.False(await _repository.CpfJaExiste(inserido.Cpf, inserido.Id));
    }

    [Fact]
    public async Task Aluno_EmailJaExiste_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        Assert.True(await _repository.EmailJaExiste(inserido.Email));
        Assert.False(await _repository.EmailJaExiste(inserido.Email, inserido.Id));
    }

    [Fact]
    public async Task Aluno_ObterPorNome_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var resultados = await _repository.ObterPorNome("Lia");
        Assert.NotEmpty(resultados);
        Assert.Contains(resultados, x => x.Id == inserido.Id);
        Assert.All(resultados, x => Assert.Contains("lia", x.Nome.ToLowerInvariant()));
    }

    [Fact]
    public async Task Aluno_TrocarSenha_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var novaSenha = Senha.Criar("novaSenha456").Value!;
        var trocada = await _repository.TrocarSenha(inserido.Id, novaSenha);
        Assert.True(trocada);

        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal(novaSenha.Valor, obtido!.Senha.Valor);
    }

    [Fact]
    public async Task Aluno_Remover_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarAluno());
        var removido = await _repository.Remover(inserido.Id);
        Assert.True(removido);
        Assert.Null(await _repository.ObterPorId(inserido.Id));
    }
}
