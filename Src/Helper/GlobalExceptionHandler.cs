using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WorkoutPlanner.Service.Exception;

namespace WorkoutPlanner.Helper;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json"; ;

        var problemDetails = new ProblemDetails();
        problemDetails.Instance = httpContext.Request.Path;

        if (exception is WorkoutNotFoundException)
        {
            httpContext.Response.StatusCode = 404;
            problemDetails.Title = exception.Message;
        }

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problemDetails), cancellationToken);
          
        return true;
    }
}