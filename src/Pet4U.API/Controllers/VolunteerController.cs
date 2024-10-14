using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Shared;
using Pet4U.Domain.ValueObjects;
using Pet4U.Response;

namespace Pet4U.API;

//[ApiController]
//[Route("[controller]")]
public class VolunteerController : ApplicationController //ControllerBase
{
  //private readonly ICreateVolunteerHandler _createVolunteerHandler;

  // public VolunteerController(ICreateVolunteerHandler createVolunteerHandler)
  // {
  //   _createVolunteerHandler = createVolunteerHandler;
  // }


  [HttpPost]
  public async Task<IActionResult> Create([FromServices] ICreateVolunteerHandler _createVolunteerHandler, [FromServices] IValidator<CreateVolunteerRequest> requestValidator,[FromBody] CreateVolunteerRequest volunteer, CancellationToken cancellationToken = default)
  {

    var validationResult = await requestValidator.ValidateAsync(volunteer, cancellationToken);
    if(!validationResult.IsValid)
    {
      var validationErrors = validationResult.Errors;
      List<ResponseError> errors = [];

      foreach(var validationError in validationErrors)
      {
        var error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage);
        errors.Add(new(error.Code, error.Message, validationError.PropertyName));
      }

      var envelope = Envelope.Error(errors);
      return BadRequest(envelope);
    }

    var volunteerCommand = volunteer.ToCommand();

    var result = await _createVolunteerHandler.HandleAsync(volunteerCommand, cancellationToken);
    
    return result.ToResponse();

    // if(result.IsFailure)
    //   return result.Error.ToResponse(); //result.Error.ToResponse();
    
    // return Ok(result.Value); //Ok(Envelope.Ok(result ));
  }
}