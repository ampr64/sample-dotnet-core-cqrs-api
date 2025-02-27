using System;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Customers.GetCustomerDetails;

public class GetCustomerDetailsQuery(Guid customerId) : IQuery<CustomerDetailsDto>
{
    public Guid CustomerId { get; } = customerId;
}