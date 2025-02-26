namespace SampleProject.Application.Configuration.Emails
{
    public struct EmailMessage(
        string from,
        string to,
        string content)
    {
        public string From { get; } = from;

        public string To { get; } = to;

        public string Content { get; } = content;
    }
}