using System;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Customers.GetCustomerDetails;

public record GetCustomerDetailsQuery(Guid CustomerId) : IQuery<CustomerDetailsDto>;