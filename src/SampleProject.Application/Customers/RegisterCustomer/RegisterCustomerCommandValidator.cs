using FluentValidation;

namespace SampleProject.Application.Customers.RegisterCustomer;

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is empty");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not a valid email address");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is empty");
    }
}