using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments;

public class PaymentId(Guid value) : TypedIdValueBase(value)
{
}