using Microsoft.AspNetCore.Mvc;
using Pet4U.Domain.Shared;

namespace Pet4U.API.Extensions;

public static class ResponseExtensions
{
  public static ActionResult ToResponse(this Error error)
  {
    //ProblemDetails

    var statusCode = error.Type switch
    {
      ErrorType.Validation => StatusCodes.Status400BadRequest,
      ErrorType.NotFound => StatusCodes.Status404NotFound,
      ErrorType.Conflict => StatusCodes.Status409Conflict,
      ErrorType.Failure => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError,
      //ErrorType.None => StatusCodes.Status200OK,
    };
    return new ObjectResult(error)
    {
      StatusCode = statusCode
    };
  }
}