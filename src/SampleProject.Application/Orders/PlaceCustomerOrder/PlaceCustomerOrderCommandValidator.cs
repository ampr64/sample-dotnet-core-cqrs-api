using FluentValidation;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Application.Orders.PlaceCustomerOrder;

public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
{
    public PlaceCustomerOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
        RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty");
        RuleForEach(x => x.Products).SetValidator(new ProductDtoValidator());

        RuleFor(x => x.Currency).Must(BeAValidCurrency)
            .WithMessage("At least one product has invalid currency");
    }

    private static bool BeAValidCurrency(string currency)
    {
        try
        {
            Currency.Of(currency);
            return true;
        }
        catch
        {
            return false;
        }
    }
}