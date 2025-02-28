using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers;
using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Customers;

[method: JsonConstructor]
public class MarkCustomerAsWelcomedCommand(Guid id, CustomerId customerId) : InternalCommandBase<Unit>(id)
{
    public CustomerId CustomerId { get; } = customerId;
}