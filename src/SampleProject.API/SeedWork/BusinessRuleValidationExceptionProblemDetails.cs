using Microsoft.AspNetCore.Mvc;
using SampleProject.Domain.SeedWork;

namespace SampleProject.API.SeedWork;

public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
{
    public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        Title = "Business rule validation error";
        Status = StatusCodes.Status409Conflict;
        Detail = exception.Details;
        Type = "https://somedomain/business-rule-validation-error";
    }
}