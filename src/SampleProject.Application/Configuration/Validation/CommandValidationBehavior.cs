using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Configuration.Validation;

public class CommandValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var errors = _validators
        .Select(v => v.Validate(request))
        .SelectMany(result => result.Errors)
        .Where(error => error != null)
        .ToList();

        if (errors.Count > 0)
        {
            var errorBuilder = new StringBuilder();

            errorBuilder.AppendLine("Invalid command, reason: ");

            foreach (var error in errors)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new InvalidCommandException(errorBuilder.ToString(), null);
        }

        return next();
    }
}