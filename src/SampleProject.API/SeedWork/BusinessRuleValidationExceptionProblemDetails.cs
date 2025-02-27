using Microsoft.AspNetCore.Mvc;
using SampleProject.Domain.SeedWork;

namespace SampleProject.API.SeedWork;

public class BusinessRuleValidationExceptionProblemDetails : ProblemDetails
{
    public BusinessRuleValidationExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        this.Title = "Business rule validation error";
        this.Status = StatusCodes.Status409Conflict;
        this.Detail = exception.Details;
        this.Type = "https://somedomain/business-rule-validation-error";
    }
}