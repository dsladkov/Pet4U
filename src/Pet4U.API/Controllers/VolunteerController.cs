using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.UseCases.DeleteVolunteer;
using Pet4U.Application.UseCases.UpdateMainInfo;
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
  public async Task<IActionResult> Create
  ([FromServices] ICreateVolunteerHandler _createVolunteerHandler, 
  //[FromServices] IValidator<CreateVolunteerRequest> requestValidator,
  [FromBody] CreateVolunteerRequest volunteer, 
  CancellationToken cancellationToken = default)
  {
    // throw new ApplicationException("Volunteers cannot be creted");
    // var validationResult = await requestValidator.ValidateAsync(volunteer, cancellationToken);
    // if(!validationResult.IsValid)
    // {
    //   return validationResult.ToValidationErrorResponse();
    //   // var validationErrors = validationResult.Errors;
    //   // List<ResponseError> errors = [];

    //   // foreach(var validationError in validationErrors)
    //   // {
    //   //   var error = Error.Validation(validationError.ErrorCode, validationError.ErrorMessage);
    //   //   errors.Add(new(error.Code, error.Message, validationError.PropertyName));
    //   // }

    //   // var errors = from validationError in validationResult.Errors

    //   //   let error  = Error.Deserialize(validationError.ErrorMessage) //Error.Validation(validationError.ErrorCode, validationError.ErrorMessage)
    //   //   select new ResponseError(error.Code, error.Message, validationError.PropertyName);
    //   //   //select new ResponseError(error.Code, error.Message, validationError.PropertyName);

    //   // var envelope = Envelope.Error(errors);
    //   // return BadRequest(envelope);
    // }

    var volunteerCommand = volunteer.ToCommand();

    var result = await _createVolunteerHandler.HandleAsync(volunteerCommand, cancellationToken);
    
    return result.ToResponse();

    // if(result.IsFailure)
    //   return result.Error.ToResponse(); //result.Error.ToResponse();
    
    // return Ok(result.Value); //Ok(Envelope.Ok(result ));
  }

  //volunteers/id/main-info
  [HttpPut("{id:guid}/main-info")]
  public async Task<IActionResult> UpdateMainInfo(
    [FromRoute] Guid id, 
    [FromBody] UpdateMainInfoDto dto,
    [FromServices] IUpdateMainInfoHandler _updateMainInfoHandler,
    [FromServices] IValidator<UpdateMainInfoVolunteerRequest> requestValidator,
    CancellationToken cancellationToken =default)
  {
    UpdateMainInfoVolunteerRequest updateMainInfoVolunteerRequest = new UpdateMainInfoVolunteerRequest(id, dto);
     var validationResult = await requestValidator.ValidateAsync(updateMainInfoVolunteerRequest, cancellationToken);
     if(validationResult.IsValid == false)
     {
      // var errors = from validationError in validationResult.Errors
 
      //             let error  = Error.Deserialize(validationError.ErrorMessage)

      //             select new ResponseError(error.Code, error.Message, validationError.PropertyName);

      //   var envelope = Envelope.Error(errors);
      //   return BadRequest(envelope);

      return validationResult.ToValidationErrorResponse();
     }
    var result = await _updateMainInfoHandler.HandleAsync(updateMainInfoVolunteerRequest.ToCommand(), cancellationToken);
    return result.ToResponse();
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> Delete(
    [FromRoute] Guid id,
    [FromServices] IDeleteVolunteerHandler _deleteVolunteerHandler,
    [FromServices] IValidator<DeleteVolunteerRequest> requestValidator,
    CancellationToken cancellationToken =default)
  {
    DeleteVolunteerRequest deleteVolunteerRequest = new DeleteVolunteerRequest(id);

    var validationResult = await requestValidator.ValidateAsync(deleteVolunteerRequest, cancellationToken);

    if(validationResult.IsValid == false)
     return validationResult.ToValidationErrorResponse();

    var result = await _deleteVolunteerHandler.HandleAsync(deleteVolunteerRequest.ToCommand(), cancellationToken);
    return result.ToResponse();
  }
}