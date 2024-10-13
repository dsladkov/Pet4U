using Microsoft.AspNetCore.Mvc;
using Pet4U.Domain.ValueObjects;
using Pet4U.Response;
using Type = Pet4U.Domain.ValueObjects.Type;

namespace Pet4U.API.Extensions;

public static class ResponseExtensions
{
  public static ActionResult ToResponse(this Error error)
  {
    //ProblemDetails

    var statusCode = error.Type switch
    {
        Type.Validation => StatusCodes.Status400BadRequest,
        Type.NotFound => StatusCodes.Status404NotFound,
        Type.Conflict => StatusCodes.Status409Conflict,
        Type.Failure => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError,
      //ErrorType.None => StatusCodes.Status200OK,
    };
    var responseError = new ResponseError(error.Code, error.Message, null);

    // var listResponseError =  new List<ResponseError>();
    // listResponseError.Add(responseError);

    var envelope = Envelope.Error([responseError]);


    return new ObjectResult(envelope)
    {
      StatusCode = statusCode
    };
  }


  public static ActionResult ToResponse(this Result result)
  {
    if(result.IsSuccess)
      return new OkObjectResult(Envelope.Ok(result));

    var statusCode = result.Error.Type switch
    {
        Type.Validation => StatusCodes.Status400BadRequest,
        Type.NotFound => StatusCodes.Status404NotFound,
        Type.Conflict => StatusCodes.Status409Conflict,
        Type.Failure => StatusCodes.Status500InternalServerError,
      _ => StatusCodes.Status500InternalServerError,
      //ErrorType.None => StatusCodes.Status200OK,
    };

    var responseError = new ResponseError(result.Error.Code, result.Error.Message, null);

    // var listResponseError =  new List<ResponseError>();
    // listResponseError.Add(responseError);

    var envelope = Envelope.Error([responseError]);

    //var envelope = Envelope.Error(result.Error);
    return new ObjectResult(envelope) //result.Error
    {
      StatusCode = statusCode
    };
  }

  // public static ActionResult<T> ToResponse<T> (this Result<T> result)
  // {
  //   //ProblemDetails

  //   var statusCode = result.Error.Type switch
  //   {
  //       Type.Validation => StatusCodes.Status400BadRequest,
  //       Type.NotFound => StatusCodes.Status404NotFound,
  //       Type.Conflict => StatusCodes.Status409Conflict,
  //       Type.Failure => StatusCodes.Status500InternalServerError,
  //     _ => StatusCodes.Status500InternalServerError,
  //     //ErrorType.None => StatusCodes.Status200OK,
  //   };
  //   return new ObjectResult(result.Error)
  //   {
  //     StatusCode = statusCode
  //   };
  // }
}