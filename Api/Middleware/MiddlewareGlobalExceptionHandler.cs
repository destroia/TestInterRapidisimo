using System.Net;

namespace Api.Middleware;

public class MiddlewareGlobalExceptionHandler
{
    readonly RequestDelegate _next;

    public MiddlewareGlobalExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsJsonAsync(new
        {
            error = "Internal Server Error",
            message = exception.Message,
        });
    }
}
