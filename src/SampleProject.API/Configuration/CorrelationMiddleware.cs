namespace SampleProject.API.Configuration;

internal class CorrelationMiddleware(
    RequestDelegate next)
{
    internal const string CorrelationHeaderKey = "CorrelationId";

    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var correlationId = Guid.NewGuid();

        if (context.Request != null)
        {
            context.Request.Headers.Add(CorrelationHeaderKey, correlationId.ToString());
        }

        await _next.Invoke(context);
    }
}