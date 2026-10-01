using AcademiaDoZe.Domain.Common;
using AcademiaDoZe.Domain.Entities;
using AcademiaDoZe.Domain.Repositories;
using AcademiaDoZe.Domain.ValueObjects;
using AcademiaDoZe.Infrastructure.Data;
using AcademiaDoZe.Infrastructure.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace AcademiaDoZe.Infrastructure.Repositories;

public sealed class LogradouroRepository : BaseRepository, ILogradouroRepository
{
    private readonly string _connectionString;
    private readonly DatabaseType _databaseType;

    public LogradouroRepository(string connectionString, DatabaseType databaseType) : base(connectionString, databaseType)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InfrastructureException("CONEXAO_STRING_VAZIA", "String de conexão não pode ser vazia.");

        if (databaseType != DatabaseType.SqlServer)
            throw new InfrastructureException("SGDB_NAO_SUPORTADO", "Este repositório está configurado para SQL Server.");

        _connectionString = connectionString;
        _databaseType = databaseType;
    }

    private async Task<SqlConnection> OpenConnectionAsync(CancellationToken cancellationToken)
    {
        var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await EnsureTableAsync(connection, cancellationToken);
        return connection;
    }

    private static async Task EnsureTableAsync(SqlConnection connection, CancellationToken cancellationToken)
    {
        const string sql = """
            IF OBJECT_ID(N'dbo.tb_logradouro', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.tb_logradouro
                (
                    id_logradouro INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
                    cep VARCHAR(8) NOT NULL,
                    nome VARCHAR(150) NOT NULL,
                    bairro VARCHAR(150) NOT NULL,
                    cidade VARCHAR(150) NOT NULL,
                    estado VARCHAR(2) NOT NULL,
                    pais VARCHAR(100) NOT NULL
                );
            END
            """;

        await using var command = new SqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private const string SelectSql = "SELECT id_logradouro, cep, nome, bairro, cidade, estado, pais FROM dbo.tb_logradouro";

    public static Logradouro Map(DbDataReader reader, string nomeColumn = "nome")
    {
        var result = Logradouro.Criar(
            reader.GetInt32Value("id_logradouro"),
            reader.GetStringValue("cep"),
            reader.GetStringValue(nomeColumn),
            reader.GetStringValue("bairro"),
            reader.GetStringValue("cidade"),
            reader.GetStringValue("estado"),
            reader.GetStringValue("pais"));

        if (result.IsFailure || result.Value is null)
            throw new InfrastructureException("ERRO_MAPEAMENTO_LOGRADOURO", "Não foi possível mapear o logradouro retornado pelo banco.");

        return result.Value;
    }

    public async Task<Logradouro?> ObterPorId(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        await using var command = new SqlCommand($"{SelectSql} WHERE id_logradouro = @Id", connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? Map(reader) : null;
    }

    public async Task<IEnumerable<Logradouro>> ObterTodos(CancellationToken cancellationToken = default)
    {
        var lista = new List<Logradouro>();
        await using var connection = await OpenConnectionAsync(cancellationToken);
        await using var command = new SqlCommand($"{SelectSql} ORDER BY id_logradouro", connection);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
            lista.Add(Map(reader));
        return lista;
    }

    public async Task<Logradouro> Adicionar(Logradouro entity, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        const string sql = """
            INSERT INTO dbo.tb_logradouro (cep, nome, bairro, cidade, estado, pais)
            OUTPUT INSERTED.id_logradouro
            VALUES (@Cep, @Nome, @Bairro, @Cidade, @Estado, @Pais);
            """;
        await using var command = new SqlCommand(sql, connection);
        AddValues(command, entity);
        var id = Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken));
        typeof(AcademiaDoZe.Domain.Entities.Entity).GetProperty("Id")?.SetValue(entity, id);
        return entity;
    }

    public async Task<Logradouro> Atualizar(Logradouro entity, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        const string sql = """
            UPDATE dbo.tb_logradouro
            SET cep = @Cep, nome = @Nome, bairro = @Bairro, cidade = @Cidade, estado = @Estado, pais = @Pais
            WHERE id_logradouro = @Id;
            """;
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = entity.Id;
        AddValues(command, entity);
        var affected = await command.ExecuteNonQueryAsync(cancellationToken);
        if (affected == 0)
            throw new InfrastructureException("REGISTRO_NAO_ENCONTRADO", $"Nenhum logradouro encontrado com ID {entity.Id}.");
        return entity;
    }

    public async Task<bool> Remover(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        await using var command = new SqlCommand("DELETE FROM dbo.tb_logradouro WHERE id_logradouro = @Id", connection);
        command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        return await command.ExecuteNonQueryAsync(cancellationToken) > 0;
    }

    public async Task<Logradouro?> ObterPorCep(Cep cep, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        await using var command = new SqlCommand($"{SelectSql} WHERE cep = @Cep", connection);
        command.Parameters.Add("@Cep", SqlDbType.VarChar, 8).Value = cep.Valor;
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        return await reader.ReadAsync(cancellationToken) ? Map(reader) : null;
    }

    public async Task<bool> CepJaExiste(Cep cep, int? id = null, CancellationToken cancellationToken = default)
    {
        await using var connection = await OpenConnectionAsync(cancellationToken);
        const string sql = "SELECT COUNT(1) FROM dbo.tb_logradouro WHERE cep = @Cep AND (@Id IS NULL OR id_logradouro <> @Id)";
        await using var command = new SqlCommand(sql, connection);
        command.Parameters.Add("@Cep", SqlDbType.VarChar, 8).Value = cep.Valor;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = (object?)id ?? DBNull.Value;
        return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0;
    }

    public async Task<IEnumerable<Logradouro>> ObterPorCidade(string cidade, CancellationToken cancellationToken = default)
        => await ObterPorFiltroAsync("cidade = @Cidade", (c, _) => c.Parameters.Add("@Cidade", SqlDbType.VarChar, 150).Value = cidade, cancellationToken);

    public async Task<IEnumerable<Logradouro>> ObterPorBairro(string cidade, string bairro, CancellationToken cancellationToken = default)
        => await ObterPorFiltroAsync("cidade = @Cidade AND bairro = @Bairro", (c, _) =>
        {
            c.Parameters.Add("@Cidade", SqlDbType.VarChar, 150).Value = cidade;
            c.Parameters.Add("@Bairro", SqlDbType.VarChar, 150).Value = bairro;
        }, cancellationToken);

    private async Task<IEnumerable<Logradouro>> ObterPorFiltroAsync(string filtro, Action<SqlCommand, object?> addParameters, CancellationToken cancellationToken)
    {
        var lista = new List<Logradouro>();
        await using var connection = await OpenConnectionAsync(cancellationToken);
        await using var command = new SqlCommand($"{SelectSql} WHERE {filtro} ORDER BY bairro, nome", connection);
        addParameters(command, null);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
            lista.Add(Map(reader));
        return lista;
    }

    private static void AddValues(SqlCommand command, Logradouro entity)
    {
        command.Parameters.Add("@Cep", SqlDbType.VarChar, 8).Value = entity.Cep.Valor;
        command.Parameters.Add("@Nome", SqlDbType.VarChar, 150).Value = entity.Nome;
        command.Parameters.Add("@Bairro", SqlDbType.VarChar, 150).Value = entity.Bairro;
        command.Parameters.Add("@Cidade", SqlDbType.VarChar, 150).Value = entity.Cidade;
        command.Parameters.Add("@Estado", SqlDbType.VarChar, 2).Value = entity.Estado;
        command.Parameters.Add("@Pais", SqlDbType.VarChar, 100).Value = entity.Pais;
    }
}
