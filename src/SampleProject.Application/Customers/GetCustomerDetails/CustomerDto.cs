using System;

namespace SampleProject.Application.Customers.GetCustomerDetails;

public record CustomerDetailsDto(Guid Id, string Name, string Email, string WelcomeEmailWasSent);
