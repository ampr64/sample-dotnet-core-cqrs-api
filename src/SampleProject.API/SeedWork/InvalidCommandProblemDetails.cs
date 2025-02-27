using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Configuration.Validation;

namespace SampleProject.API.SeedWork;

public class InvalidCommandProblemDetails : ProblemDetails
{
    public InvalidCommandProblemDetails(InvalidCommandException exception)
    {
        this.Title = exception.Message;
        this.Status = StatusCodes.Status400BadRequest;
        this.Detail = exception.Details;
        this.Type = "https://somedomain/validation-error";
    }
}