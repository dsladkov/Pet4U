using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Pet4U.Domain.Shared;
using Pet4U.Response;
using Type = Pet4U.Domain.Shared.Type;

namespace Pet4U.API.Extensions;

public static class ResponseExtensions
{

  public static ActionResult ToValidationErrorResponse(this FluentValidation.Results.ValidationResult validationResult)
  {
    if(validationResult.IsValid)
      throw new InvalidOperationException("Result cannot be succeed");

    
      // var validationErrors = validationResult.Errors;
      // List<ResponseError> errors = [];

      // foreach(var validationError in validationErrors)
      // {
      //   var error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage);
      //   errors.Add(new(error.Code, error.Message, validationError.PropertyName));
      // }

      var errors = from validationError in validationResult.Errors

        let error  = Error.Deserialize(validationError.ErrorMessage) //Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
        select new ResponseError(error.Code, error.Message, validationError.PropertyName);
        //select new ResponseError(error.Code, error.Message, validationError.PropertyName);

      var envelope = Envelope.Error(errors);
      return new ObjectResult(envelope)
      {
        StatusCode = StatusCodes.Status400BadRequest
      };

  }
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