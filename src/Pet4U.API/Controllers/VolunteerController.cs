using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain;
using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;
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
  public async Task<IActionResult> Create([FromServices] ICreateVolunteerHandler _createVolunteerHandler, [FromBody] CreateVolunteerRequest volunteer, CancellationToken cancellationToken = default)
  {
    var volunteerCommand = volunteer.ToCommand();

    var result = await _createVolunteerHandler.HandleAsync(volunteerCommand, cancellationToken);
    
    return result.ToResponse();

    // if(result.IsFailure)
    //   return result.Error.ToResponse(); //result.Error.ToResponse();
    
    // return Ok(result.Value); //Ok(Envelope.Ok(result ));
  }
}