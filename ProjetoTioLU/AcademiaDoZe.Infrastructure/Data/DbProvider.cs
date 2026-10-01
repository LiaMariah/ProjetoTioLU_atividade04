using AcademiaDoZe.Infrastructure.Exceptions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace AcademiaDoZe.Infrastructure.Data;

public static class DbProvider
{
    public const int DefaultCommandTimeout = 30;

    public static DbConnection CreateConnection(
        string connectionString,
        DatabaseType databaseType)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InfrastructureException(
                "CONEXAO_STRING_VAZIA",
                "String de conexão não pode ser vazia.");
        }

        if (databaseType != DatabaseType.SqlServer)
        {
            throw new InfrastructureException(
                "SGDB_NAO_SUPORTADO",
                "Neste momento, a atividade está configurada para SQL Server.");
        }

        return new SqlConnection(connectionString);
    }

    public static DbCommand CreateCommand(
        string commandText,
        DbConnection connection)
    {
        if (string.IsNullOrWhiteSpace(commandText))
        {
            throw new InfrastructureException(
                "SQL_VAZIO",
                "O comando SQL não pode ser vazio.");
        }

        var command = connection.CreateCommand();
        command.CommandText = commandText;
        command.CommandType = CommandType.Text;
        command.CommandTimeout = DefaultCommandTimeout;
        return command;
    }

    public static DbParameter AddParameter(
        DbCommand command,
        string name,
        object? value)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        parameter.Value = value ?? DBNull.Value;
        command.Parameters.Add(parameter);
        return parameter;
    }
}
