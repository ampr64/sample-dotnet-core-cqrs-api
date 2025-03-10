using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Customers;
using System;
using System.Text.Json.Serialization;

namespace SampleProject.Application.Customers;

[method: JsonConstructor]
public record MarkCustomerAsWelcomedCommand(Guid Id, CustomerId CustomerId)
    : InternalCommandBase(Id);