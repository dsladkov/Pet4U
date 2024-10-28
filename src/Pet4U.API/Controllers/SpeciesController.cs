using Microsoft.AspNetCore.Mvc;
using Pet4U.Application.UseCases.CreateSpecies;

namespace Pet4U.API.Controllers;

public class SpeciesController : ApplicationController
{
  [HttpPost]
  public async Task<IActionResult> Create ([FromBody] CreateSpeciesRequest request, CancellationToken cancellationToken = default)
  {
    var command = CreateSpeciesRequest.ToCommand(request.Title, request.Description);
    return Ok();
  }
}