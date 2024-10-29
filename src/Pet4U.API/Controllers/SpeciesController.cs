using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.AddBreeds;
using Pet4U.Application.UseCases.CreateSpecies;

namespace Pet4U.API.Controllers;

public class SpeciesController : ApplicationController
{
  [HttpPost]
  public async Task<IActionResult> Create ([FromBody] CreateSpeciesRequest request, [FromServices] ICreateSpeciesHandler speciesHandler, CancellationToken cancellationToken = default)
  {
    var command = CreateSpeciesRequest.ToCommand(request.title, request.description);

    var result = await speciesHandler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }


  [HttpPost("{id:guid}/breeds")]
  public async Task<IActionResult> AddBreeds ([FromRoute] Guid id, [FromBody] AddBreedsRequest request, [FromServices] IAddBreedsHandler addBreedsHandler, CancellationToken cancellationToken = default)
  {
    var command =  AddBreedsRequest.ToCommand(id, request.breeds);

    var result = await addBreedsHandler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }
}