using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderId(Guid value) : TypedIdValueBase(value)
    {
    }
}