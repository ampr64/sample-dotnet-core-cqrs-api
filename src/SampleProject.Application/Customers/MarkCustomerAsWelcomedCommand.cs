using System;
using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers;

namespace SampleProject.Application.Customers;

[method: JsonConstructor]
public class MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : InternalCommandBase<Unit>(id)
{
    public CustomerId CustomerId { get; } = customerId;
}