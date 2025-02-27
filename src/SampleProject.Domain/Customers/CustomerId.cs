using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers;

public class CustomerId(Guid value) : TypedIdValueBase(value)
{
}