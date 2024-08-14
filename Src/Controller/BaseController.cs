using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WorkoutPlanner.Controller;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected static Ok<T> Ok<T>(T? value)
    {
        return TypedResults.Ok(value);
    }
    
    protected new static NoContent NoContent()
    {
        return TypedResults.NoContent();
    }

    protected new static NotFound NotFound()
    {
        return TypedResults.NotFound();
    }

    protected static BadRequest<TValue> BadRequest<TValue>(TValue? value)
    {
        return TypedResults.BadRequest(value);
    }
}