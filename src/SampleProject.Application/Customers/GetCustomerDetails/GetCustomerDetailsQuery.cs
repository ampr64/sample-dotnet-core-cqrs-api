using SampleProject.Application.Configuration.Queries;
using System;

namespace SampleProject.Application.Customers.GetCustomerDetails;

public record GetCustomerDetailsQuery(Guid CustomerId) : IQuery<CustomerDetailsDto>;