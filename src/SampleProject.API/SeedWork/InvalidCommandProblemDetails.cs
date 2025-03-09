using Microsoft.AspNetCore.Mvc;
using SampleProject.Application.Configuration.Validation;

namespace SampleProject.API.SeedWork;

public class InvalidCommandProblemDetails : ProblemDetails
{
    public InvalidCommandProblemDetails(InvalidCommandException exception)
    {
        Title = exception.Message;
        Status = StatusCodes.Status400BadRequest;
        Detail = exception.Details;
        Type = "https://somedomain/validation-error";
    }
}