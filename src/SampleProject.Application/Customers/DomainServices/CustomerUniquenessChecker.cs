using Dapper;
using SampleProject.Application.Configuration.Data;
using SampleProject.Domain.Customers;

namespace SampleProject.Application.Customers.DomainServices;

public class CustomerUniquenessChecker(ISqlConnectionFactory sqlConnectionFactory) : ICustomerUniquenessChecker
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory = sqlConnectionFactory;

    public bool IsUnique(string customerEmail)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = """
                           SELECT TOP 1 1
                           FROM [orders].[Customers] AS [Customer]
                           WHERE [Customer].[Email] = @Email
                           """;
        var customersNumber = connection.QuerySingleOrDefault<int?>(sql,
                        new
                        {
                            Email = customerEmail
                        });

        return !customersNumber.HasValue;
    }
}