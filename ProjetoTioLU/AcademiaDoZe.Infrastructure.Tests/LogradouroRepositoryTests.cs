using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.ValueObjects;
using AcademiaDoZe.Infrastructure.Data;
using AcademiaDoZe.Infrastructure.Repositories;

namespace AcademiaDoZe.Infrastructure.Tests;

public sealed class LogradouroRepositoryTests
{
    private const string ConnectionString =
        "Server=localhost,1433;Database=db_academia_do_ze;User Id=sa;Password=abcBolinhas12345;TrustServerCertificate=True;Encrypt=True;";

    private readonly LogradouroRepository _repository =
        new(ConnectionString, DatabaseType.SqlServer);

    private static string GerarCep() =>
        Random.Shared.Next(10000000, 99999999).ToString();

    private static Logradouro CriarLogradouro()
    {
        var resultado = Logradouro.Criar(
            0,
            GerarCep(),
            "Lia Mariah",
            "Olivo",
            "SQLServer",
            "SC",
            "Brasil");

        Assert.True(resultado.IsSuccess, string.Join("; ", resultado.Notifications));
        return resultado.Value!;
    }

    [Fact]
    public async Task Logradouro_Adicionar_Sucesso()
    {
        var logradouro = await _repository.Adicionar(CriarLogradouro());
        Assert.True(logradouro.Id > 0);
        Assert.Equal("Lia Mariah", logradouro.Nome);
        Assert.Equal("Olivo", logradouro.Bairro);
        Assert.Equal("SQLServer", logradouro.Cidade);
    }

    [Fact]
    public async Task Logradouro_ObterPorId_Sucesso()
    {
        var inserido = await _repository.Adicionar(CriarLogradouro());
        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
        Assert.Equal("SQLServer", obtido.Cidade);
    }

    [Fact]
    public async Task Logradouro_ObterTodos_Sucesso()
    {
        await _repository.Adicionar(CriarLogradouro());
        var todos = await _repository.ObterTodos();
        Assert.NotEmpty(todos);
    }

    [Fact]
    public async Task Logradouro_Atualizar_Sucesso()
    {
        var inserido = await _repository.Adicionar(CriarLogradouro());
        var atualizado = Logradouro.Criar(
            inserido.Id,
            GerarCep(),
            "Lia Mariah",
            "Olivo",
            "SQLServer",
            "SC",
            "Brasil").Value!;

        var resultado = await _repository.Atualizar(atualizado);
        Assert.Equal("Lia Mariah", resultado.Nome);
        Assert.Equal("Olivo", resultado.Bairro);
    }

    [Fact]
    public async Task Logradouro_ObterPorCep_Sucesso()
    {
        var inserido = await _repository.Adicionar(CriarLogradouro());
        var obtido = await _repository.ObterPorCep(inserido.Cep);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
    }

    [Fact]
    public async Task Logradouro_CepJaExiste_Sucesso()
    {
        var inserido = await _repository.Adicionar(CriarLogradouro());
        Assert.True(await _repository.CepJaExiste(inserido.Cep));
        Assert.False(await _repository.CepJaExiste(inserido.Cep, inserido.Id));
    }

    [Fact]
    public async Task Logradouro_ObterPorCidade_Sucesso()
    {
        await _repository.Adicionar(CriarLogradouro());
        var resultados = await _repository.ObterPorCidade("SQLServer");
        Assert.NotEmpty(resultados);
        Assert.Contains(resultados, x => x.Cidade == "SQLServer");
    }

    [Fact]
    public async Task Logradouro_ObterPorBairro_Sucesso()
    {
        await _repository.Adicionar(CriarLogradouro());
        var resultados = await _repository.ObterPorBairro("SQLServer", "Olivo");
        Assert.NotEmpty(resultados);
        Assert.Contains(resultados, x => x.Bairro == "Olivo");
    }

    [Fact]
    public async Task Logradouro_Remover_Sucesso()
    {
        var inserido = await _repository.Adicionar(CriarLogradouro());
        var removido = await _repository.Remover(inserido.Id);
        Assert.True(removido);
        Assert.Null(await _repository.ObterPorId(inserido.Id));
    }
}
