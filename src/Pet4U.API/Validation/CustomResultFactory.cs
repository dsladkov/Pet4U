using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pet4U.Domain.Shared;
using Pet4U.Response;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Pet4U.API.Validation;

public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context, ValidationProblemDetails? validationProblemDetails)
    {
        if(validationProblemDetails is null)
            throw new InvalidOperationException("ValidationProblemDetails is null");

            List<ResponseError> errorsList = [];

        foreach(var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var errors = from errorMessage in validationErrors

            let error  = Error.Deserialize(errorMessage)
            select new ResponseError(error.Code, error.Message, invalidField);
            errorsList.AddRange(errors);
        }

        var envelope = Envelope.Error(errorsList);

        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}