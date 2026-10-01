using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.ValueObjects;
using AcademiaDoZe.Infrastructure.Data;
using AcademiaDoZe.Infrastructure.Repositories;

namespace AcademiaDoZe.Infrastructure.Tests;

public sealed class ColaboradorRepositoryTests : TestBase
{
    private readonly ColaboradorRepository _repository;

    public ColaboradorRepositoryTests()
    {
        _repository = new ColaboradorRepository(ConnectionString, DatabaseType);
    }

    private async Task<Colaborador> CriarColaborador()
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

        await using var logradouroRepository = new LogradouroRepository(ConnectionString, DatabaseType);
        var logradouro = await logradouroRepository.Adicionar(resultadoLogradouro.Value!);

        var resultado = Colaborador.Criar(
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
            null,
            new DateOnly(2020, 1, 1),
            ColaboradorTipo.Instrutor,
            ColaboradorVinculo.CLT);

        Assert.True(resultado.IsSuccess, string.Join("; ", resultado.Notifications));
        return resultado.Value!;
    }

    [Fact]
    public async Task Colaborador_Adicionar_Sucesso()
    {
        var colaborador = await _repository.Adicionar(await CriarColaborador());
        Assert.True(colaborador.Id > 0);
        Assert.Equal("Lia Mariah", colaborador.Nome);

        var obtido = await _repository.ObterPorId(colaborador.Id);
        Assert.NotNull(obtido);
        Assert.Equal(colaborador.Cpf, obtido!.Cpf);
        Assert.Equal(colaborador.Endereco.Logradouro.Id, obtido.Endereco.Logradouro.Id);
    }

    [Fact]
    public async Task Colaborador_ObterPorId_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
        Assert.Equal("Lia Mariah", obtido.Nome);
    }

    [Fact]
    public async Task Colaborador_ObterTodos_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var todos = await _repository.ObterTodos();
        Assert.NotEmpty(todos);
        Assert.Contains(todos, x => x.Id == inserido.Id);
    }

    [Fact]
    public async Task Colaborador_Atualizar_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var atualizado = Colaborador.Criar(
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
            Arquivo.Criar(new byte[] { 1, 2, 3 }).Value!,
            new DateOnly(2021, 1, 1),
            ColaboradorTipo.Atendente,
            ColaboradorVinculo.Estagio);

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
        Assert.Equal(new DateOnly(2021, 1, 1), obtido.DataAdmissao);
        Assert.Equal(ColaboradorTipo.Atendente, obtido.Tipo);
        Assert.Equal(ColaboradorVinculo.Estagio, obtido.Vinculo);
    }

    [Fact]
    public async Task Colaborador_ObterPorCpf_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var obtido = await _repository.ObterPorCpf(inserido.Cpf);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
    }

    [Fact]
    public async Task Colaborador_ObterPorEmail_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var obtido = await _repository.ObterPorEmail(inserido.Email);
        Assert.NotNull(obtido);
        Assert.Equal(inserido.Id, obtido!.Id);
    }

    [Fact]
    public async Task Colaborador_CpfJaExiste_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        Assert.True(await _repository.CpfJaExiste(inserido.Cpf));
        Assert.False(await _repository.CpfJaExiste(inserido.Cpf, inserido.Id));
    }

    [Fact]
    public async Task Colaborador_EmailJaExiste_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        Assert.True(await _repository.EmailJaExiste(inserido.Email));
        Assert.False(await _repository.EmailJaExiste(inserido.Email, inserido.Id));
    }

    [Fact]
    public async Task Colaborador_ObterPorTipo_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var resultados = await _repository.ObterPorTipo(ColaboradorTipo.Instrutor);
        Assert.NotEmpty(resultados);
        Assert.Contains(resultados, x => x.Id == inserido.Id);
        Assert.All(resultados, x => Assert.Equal(ColaboradorTipo.Instrutor, x.Tipo));
    }

    [Fact]
    public async Task Colaborador_ObterPorVinculo_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var resultados = await _repository.ObterPorVinculo(ColaboradorVinculo.CLT);
        Assert.NotEmpty(resultados);
        Assert.Contains(resultados, x => x.Id == inserido.Id);
        Assert.All(resultados, x => Assert.Equal(ColaboradorVinculo.CLT, x.Vinculo));
    }

    [Fact]
    public async Task Colaborador_TrocarSenha_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var novaSenha = Senha.Criar("novaSenha456").Value!;
        var trocada = await _repository.TrocarSenha(inserido.Id, novaSenha);
        Assert.True(trocada);

        var obtido = await _repository.ObterPorId(inserido.Id);
        Assert.NotNull(obtido);
        Assert.Equal(novaSenha.Valor, obtido!.Senha.Valor);
    }

    [Fact]
    public async Task Colaborador_Remover_Sucesso()
    {
        var inserido = await _repository.Adicionar(await CriarColaborador());
        var removido = await _repository.Remover(inserido.Id);
        Assert.True(removido);
        Assert.Null(await _repository.ObterPorId(inserido.Id));
    }
}
