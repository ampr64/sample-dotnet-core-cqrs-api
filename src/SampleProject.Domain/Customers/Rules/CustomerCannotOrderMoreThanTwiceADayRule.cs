using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Rules;

public class CustomerCannotOrderMoreThanTwiceADayRule(IEnumerable<Order> orders) : IBusinessRule
{
    public bool IsBroken() => orders.Count(x => x.IsOrderedToday()) >= 2;

    public string Message => "You cannot order more than twice on the same day.";
}