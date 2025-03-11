namespace SampleProject.Application.Configuration.Emails;

public readonly record struct EmailMessage(string From,
    string To,
    string Content);