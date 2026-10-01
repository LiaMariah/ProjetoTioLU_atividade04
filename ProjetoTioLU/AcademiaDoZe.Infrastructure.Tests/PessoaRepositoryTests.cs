using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Infrastructure.Data;
using AcademiaDoZe.Infrastructure.Exceptions;
using AcademiaDoZe.Infrastructure.Repositories;
using System.Data;

namespace AcademiaDoZe.Infrastructure.Tests;

public sealed class PessoaRepositoryTests : TestBase
{
    private static DataTable CriarDados(bool colaborador, bool comFoto)
    {
        var tabela = new DataTable();
        tabela.Columns.Add(colaborador ? "id_colaborador" : "id_aluno", typeof(int));
        tabela.Columns.Add("cpf", typeof(string));
        tabela.Columns.Add("nome", typeof(string));
        tabela.Columns.Add("nascimento", typeof(DateTime));
        tabela.Columns.Add("telefone", typeof(string));
        tabela.Columns.Add("email", typeof(string));
        tabela.Columns.Add("numero", typeof(string));
        tabela.Columns.Add("complemento", typeof(string));
        tabela.Columns.Add("senha", typeof(string));
        tabela.Columns.Add("foto", typeof(byte[]));
        tabela.Columns.Add("id_logradouro", typeof(int));
        tabela.Columns.Add("cep", typeof(string));
        tabela.Columns.Add("logradouro_nome", typeof(string));
        tabela.Columns.Add("bairro", typeof(string));
        tabela.Columns.Add("cidade", typeof(string));
        tabela.Columns.Add("estado", typeof(string));
        tabela.Columns.Add("pais", typeof(string));
        tabela.Columns.Add("admissao", typeof(DateTime));
        tabela.Columns.Add("tipo", typeof(int));
        tabela.Columns.Add("vinculo", typeof(int));

        tabela.Rows.Add(7, "52998224725", "Maria Silva", new DateTime(2000, 1, 1),
            "48999999999", "maria@exemplo.com", "10", DBNull.Value, "abc123",
            comFoto ? new byte[] { 1, 2, 3 } : DBNull.Value,
            12, "88520000", "Rua das Flores", "Centro", "Lages", "SC", "Brasil",
            new DateTime(2020, 1, 1), (int)ColaboradorTipo.Instrutor, (int)ColaboradorVinculo.CLT);

        return tabela;
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(false, true)]
    [InlineData(true, false)]
    [InlineData(true, true)]
    public void Map_SeparaNomeDaPessoaEDoLogradouro(bool colaborador, bool comFoto)
    {
        using var tabela = CriarDados(colaborador, comFoto);
        using var reader = tabela.CreateDataReader();
        Assert.True(reader.Read());

        Pessoa pessoa = colaborador ? ColaboradorRepository.Map(reader) : AlunoRepository.Map(reader);

        Assert.Equal(7, pessoa.Id);
        Assert.Equal("Maria Silva", pessoa.Nome);
        Assert.Equal(new DateOnly(2000, 1, 1), pessoa.DataNascimento);
        Assert.Equal(12, pessoa.Endereco.Logradouro.Id);
        Assert.Equal("Rua das Flores", pessoa.Endereco.Logradouro.Nome);
        Assert.Equal(string.Empty, pessoa.Endereco.Complemento);
        if (comFoto)
            Assert.Equal(new byte[] { 1, 2, 3 }, pessoa.Foto!.Conteudo);
        else
            Assert.Null(pessoa.Foto);

        if (colaborador)
        {
            var funcionario = Assert.IsType<Colaborador>(pessoa);
            Assert.Equal(new DateOnly(2020, 1, 1), funcionario.DataAdmissao);
            Assert.Equal(ColaboradorTipo.Instrutor, funcionario.Tipo);
            Assert.Equal(ColaboradorVinculo.CLT, funcionario.Vinculo);
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Map_RejeitaDadosInvalidos(bool colaborador)
    {
        using var tabela = CriarDados(colaborador, false);
        tabela.Rows[0]["cpf"] = "00000000000";
        using var reader = tabela.CreateDataReader();
        Assert.True(reader.Read());

        Assert.Throws<InfrastructureException>(() =>
        {
            if (colaborador)
                ColaboradorRepository.Map(reader);
            else
                AlunoRepository.Map(reader);
        });
    }

    [Fact]
    public void ObterScript_SqlServer_EncontraRecursoEmbarcado()
    {
        var script = DbInitializer.ObterScript(DatabaseType.SqlServer);
        Assert.Contains("CREATE TABLE dbo.tb_aluno", script);
        Assert.Contains("CREATE TABLE dbo.tb_colaborador", script);
        Assert.Contains("CREATE TABLE dbo.tb_logradouro", script);
    }
}
