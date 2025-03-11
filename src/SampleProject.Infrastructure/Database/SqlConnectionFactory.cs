using Microsoft.Data.SqlClient;
using SampleProject.Application.Configuration.Data;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace SampleProject.Infrastructure.Database;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable
{
    private readonly string _connectionString = connectionString;
    private SqlConnection? _connection;

    [MemberNotNullWhen(true, nameof(_connection))]
    private bool IsConnectionOpen => _connection is { State: ConnectionState.Open };

    public IDbConnection GetOpenConnection()
    {
        if (!IsConnectionOpen)
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        return _connection;
    }

    public void Dispose()
    {
        if (IsConnectionOpen)
        {
            _connection.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
