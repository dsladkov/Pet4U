using System.Net;
using Pet4U.Response;

namespace Pet4U.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
          // Call the next delegate/middleware in the pipeline.
          await _next(context); //call all software

        }
        catch (Exception ex)
        {
          _logger.LogError(ex, ex.Message);
          var responseError = new ResponseError("server.internal", ex.Message, null);
          var envelope = Envelope.Error([responseError]);
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = StatusCodes.Status500InternalServerError;
          await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
  public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder buidler)
  {
    return buidler.UseMiddleware<ExceptionMiddleware>();
  }
}