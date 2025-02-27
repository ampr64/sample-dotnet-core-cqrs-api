using System.Data;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Data.SqlClient;
using SampleProject.Application.Configuration.Data;

namespace SampleProject.Infrastructure.Database;

public class SqlConnectionFactory(string connectionString) : ISqlConnectionFactory, IDisposable
{
    private readonly string _connectionString = connectionString;
    private SqlConnection? _connection;

    [MemberNotNullWhen(true, nameof(_connection))]
    private bool IsConnectionOpen => this._connection is { State: ConnectionState.Open };

    public IDbConnection GetOpenConnection()
    {
        if (!IsConnectionOpen)
        {
            this._connection = new SqlConnection(_connectionString);
            this._connection.Open();
        }

        return this._connection;
    }

    public void Dispose()
    {
        if (IsConnectionOpen)
        {
            this._connection.Dispose();
        }

        GC.SuppressFinalize(this);
    }
}
