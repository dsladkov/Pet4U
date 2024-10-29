using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.CreateSpecies;

namespace Pet4U.API.Controllers;

public class SpeciesController : ApplicationController
{
  [HttpPost]
  public async Task<IActionResult> Create ([FromBody] CreateSpeciesRequest request, [FromServices] ICreateSpeciesHandler speciesHandler, CancellationToken cancellationToken = default)
  {
    var command = CreateSpeciesRequest.ToCommand(request.Title, request.Description);

    var result = await speciesHandler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }
}