namespace sentry_core.Middlewares;

public class SentryReporterMiddleware
{
    private readonly RequestDelegate _next;

    public SentryReporterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);
        
        var statusCode = context.Response.StatusCode;
        
        if (statusCode >= 500 && statusCode < 600)
        {
            SentrySdk.CaptureMessage($"Path: {context.Request.Path} , Status : {context.Response.StatusCode} )", scope =>
            {
                scope.SetFingerprint(
                [
                    "middleware-captured-from-500-to-600",
                    context.Request.Method,
                    context.Request.Path
                ]);
            });
        }
    }
}