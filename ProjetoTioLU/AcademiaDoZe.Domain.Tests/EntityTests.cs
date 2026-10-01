// Lia Mariah Couto Olivo
using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Enums;
using AcademiaDoZe.Domain.ValueObjects;
using Xunit;

namespace AcademiaDoZe.Domain.Tests;

public class EntityTests
{
    private static Logradouro GetValidLogradouro() =>
        Logradouro.Criar(1, "88520000", "Rua Central", "Centro", "Criciuma", "SC", "Brasil").Value!;

    private static Aluno GetValidAluno() =>
        Aluno.Criar(
            1,
            "Aluno Teste",
            "529.982.247-25",
            DateOnly.FromDateTime(DateTime.Today.AddYears(-20)),
            "48999999999",
            "aluno@example.com",
            GetValidLogradouro(),
            "10",
            "",
            "abcdef").Value!;

    private static Colaborador GetValidColaborador() =>
        Colaborador.Criar(
            2,
            "Colaborador Teste",
            "529.982.247-25",
            DateOnly.FromDateTime(DateTime.Today.AddYears(-30)),
            "48988888888",
            "colaborador@example.com",
            GetValidLogradouro(),
            "10",
            "",
            "abcdef",
            null,
            DateOnly.FromDateTime(DateTime.Today.AddYears(-2)),
            ColaboradorTipo.Atendente,
            ColaboradorVinculo.CLT).Value!;

    [Fact]
    public void Logradouro_Valido_DeveCriar() =>
        Assert.True(GetValidLogradouro() is not null);

    [Fact]
    public void Aluno_Valido_DeveCriar() =>
        Assert.Equal(1, GetValidAluno().Id);

    [Fact]
    public void Colaborador_Valido_DeveCriar() =>
        Assert.Equal(2, GetValidColaborador().Id);

    [Fact]
    public void AcessoAluno_EmHorarioDeAbertura_DeveCriar()
    {
        var result = AcessoAluno.Criar(1, GetValidAluno(), DateTime.Today.AddHours(6));
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Value!.AlunoId);
    }

    [Fact]
    public void AcessoAluno_EmHorarioDeFechamento_DeveCriar()
    {
        var result = AcessoAluno.Criar(1, GetValidAluno(), DateTime.Today.AddHours(22));
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void AcessoAluno_AntesDasSeis_DeveFalhar()
    {
        var result = AcessoAluno.Criar(1, GetValidAluno(), DateTime.Today.AddHours(5).AddMinutes(59));
        Assert.Contains(result.Notifications, n => n.Mensagem == "DATA_HORA_INTERVALO_INVALIDO");
    }

    [Fact]
    public void AcessoAluno_AposAsDezDaNoite_DeveFalhar()
    {
        var result = AcessoAluno.Criar(1, GetValidAluno(), DateTime.Today.AddHours(22).AddMinutes(1));
        Assert.Contains(result.Notifications, n => n.Mensagem == "DATA_HORA_INTERVALO_INVALIDO");
    }

    [Fact]
    public void AcessoAluno_SemAluno_DeveFalhar()
    {
        var result = AcessoAluno.Criar(1, null, DateTime.Today.AddHours(10));
        Assert.Contains(result.Notifications, n => n.Mensagem == "ALUNO_INVALIDO");
    }

    [Fact]
    public void AcessoColaborador_Valido_DeveCriar()
    {
        var result = AcessoColaborador.Criar(1, GetValidColaborador(), DateTime.Today.AddHours(10));
        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value!.ColaboradorId);
    }

    [Fact]
    public void AcessoColaborador_SemColaborador_DeveFalhar()
    {
        var result = AcessoColaborador.Criar(1, null, DateTime.Today.AddHours(10));
        Assert.Contains(result.Notifications, n => n.Mensagem == "COLABORADOR_INVALIDO");
    }

    [Theory]
    [InlineData(5, 59)]
    [InlineData(22, 1)]
    public void AcessoColaborador_ForaDoHorario_DeveFalhar(int hora, int minuto)
    {
        var result = AcessoColaborador.Criar(1, GetValidColaborador(), DateTime.Today.AddHours(hora).AddMinutes(minuto));
        Assert.Contains(result.Notifications, n => n.Mensagem == "DATA_HORA_INTERVALO_INVALIDO");
    }

    [Theory]
    [InlineData(MatriculaPlano.Mensal, 1)]
    [InlineData(MatriculaPlano.Trimestral, 3)]
    [InlineData(MatriculaPlano.Semestral, 6)]
    [InlineData(MatriculaPlano.Anual, 12)]
    public void Matricula_CalculaDataFimConformePlano(MatriculaPlano plano, int meses)
    {
        var inicio = new DateOnly(2026, 1, 10);
        var result = Matricula.Criar(1, GetValidAluno(), plano, inicio, "Hipertrofia", MatriculaRestricoes.None, null);
        Assert.True(result.IsSuccess);
        Assert.Equal(inicio.AddMonths(meses), result.Value!.DataFim);
    }

    [Fact]
    public void Matricula_ComRestricaoSemLaudo_DeveFalhar()
    {
        var result = Matricula.Criar(1, GetValidAluno(), MatriculaPlano.Mensal, new DateOnly(2026, 1, 10), "Saude", MatriculaRestricoes.Diabetes, null);
        Assert.Contains(result.Notifications, n => n.Mensagem == "RESTRICOES_LAUDO_OBRIGATORIO");
    }

    [Fact]
    public void Matricula_SemAluno_DeveFalhar()
    {
        var result = Matricula.Criar(1, null, MatriculaPlano.Mensal, new DateOnly(2026, 1, 10), "Saude", MatriculaRestricoes.None, null);
        Assert.Contains(result.Notifications, n => n.Mensagem == "ALUNO_INVALIDO");
    }

    [Fact]
    public void Matricula_ObjetivoVazio_DeveFalhar()
    {
        var result = Matricula.Criar(1, GetValidAluno(), MatriculaPlano.Mensal, new DateOnly(2026, 1, 10), "", MatriculaRestricoes.None, null);
        Assert.Contains(result.Notifications, n => n.Mensagem == "OBJETIVO_OBRIGATORIO");
    }
}
