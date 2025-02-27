using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Products;

public class ProductId(Guid value) : TypedIdValueBase(value)
{
}