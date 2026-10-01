using AcademiaDoZe.Infrastructure.Data;

[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly, DisableTestParallelization = true)]

namespace AcademiaDoZe.Infrastructure.Tests;

public abstract class TestBase
{
    // Altere o SGBD alvo dos testes trocando apenas a constante abaixo:
    private const DatabaseType SelectedDatabaseType = DatabaseType.SqlServer;

    protected string ConnectionString { get; }
    protected DatabaseType DatabaseType { get; }

    protected TestBase()
    {
        DatabaseType = SelectedDatabaseType;

        // Ajuste a ConnectionString com caminhos e credenciais válidas
        ConnectionString = DatabaseType switch
        {
            DatabaseType.SqlServer => "Server=localhost,1433;Database=db_academia_do_ze;User Id=sa;Password=abcBolinhas12345;TrustServerCertificate=True;Encrypt=True;",
            DatabaseType.MySql => "Server=localhost;Database=db_academia_do_ze;User Id=root;Password=abcBolinhas12345;",
            DatabaseType.Sqlite => @"Data Source=C:\DEV\AcademiaDoZe\db_academia_do_ze.db;Cache=Shared;",
            _ => throw new ArgumentOutOfRangeException(nameof(DatabaseType), "SGBD não suportado para testes.")
        };
    }

    #region Geradores de dados aleatórios
    private static int _counter = 10000;
    protected static string GerarCep() => (80000000 + ((int)(DateTime.UtcNow.Ticks % 8000000)) + Interlocked.Increment(ref _counter)).ToString("D8")[..8];
    protected static string GerarEmail() => $"user_{Guid.NewGuid().ToString("N")[..8]}@test.com";
    protected static string GerarTelefone() => (49990000000L + (DateTime.UtcNow.Ticks % 800000000L) + Interlocked.Increment(ref _counter)).ToString("D11")[..11];

    protected static string GerarCpf()
    {
        var cpf = Random.Shared.Next(100000000, 999999999).ToString();

        // Calcula os dois dígitos verificadores para criar um CPF válido.
        for (int tamanho = 9; tamanho <= 10; tamanho++)
        {
            int soma = 0;
            for (int i = 0; i < tamanho; i++)
                soma += (cpf[i] - '0') * (tamanho + 1 - i);

            int digito = 11 - soma % 11;
            cpf += digito >= 10 ? "0" : digito.ToString();
        }

        return cpf;
    }

    #endregion
}
