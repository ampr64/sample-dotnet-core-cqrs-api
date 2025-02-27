using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.SharedKernel;

public class MoneyValueOperationMustBePerformedOnTheSameCurrencyRule(MoneyValue left, MoneyValue right) : IBusinessRule
{
    private readonly MoneyValue _left = left;

    private readonly MoneyValue _right = right;

    public bool IsBroken() => _left.Currency != _right.Currency;

    public string Message => "Money value currencies must be the same";
}