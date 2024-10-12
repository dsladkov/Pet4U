using Microsoft.AspNetCore.Mvc;
using Pet4U.API.Extensions;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain;
using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;
using Pet4U.Response;

namespace Pet4U.API;

[ApiController]
[Route("[controller]")]
public abstract class ApplicationController : ControllerBase
{
  public override OkObjectResult Ok(object? value)
  {
    var envelope = Envelope.Ok(value);
    return new(envelope);
  }
}