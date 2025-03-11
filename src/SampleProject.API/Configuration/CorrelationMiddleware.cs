namespace SampleProject.API.Configuration;

internal class CorrelationMiddleware(
    RequestDelegate next)
{
    internal const string CorrelationHeaderKey = "CorrelationId";

    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var correlationId = Guid.NewGuid();

        context.Request?.Headers.Append(CorrelationHeaderKey, correlationId.ToString());

        await _next.Invoke(context);
    }
}