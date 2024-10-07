using Microsoft.AspNetCore.Mvc;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain;
using Pet4U.Domain.Modules;

namespace Pet4U.API;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
  private readonly CreateVolunteerHandler _createVolunteerHandler;


  public VolunteerController(CreateVolunteerHandler createVolunteerHandler)
  {
    _createVolunteerHandler = createVolunteerHandler;
  }


  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateVolunteerRequest volunteer, CancellationToken cancellationToken = default)
  {
    var volunteerCommand = new CreateVolunteerCommand(volunteer);

     var result = await _createVolunteerHandler.HandleAsync(volunteerCommand, cancellationToken);
    
    return Ok(result);
  }
}