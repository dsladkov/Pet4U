using CommunityToolkit.HighPerformance.Helpers;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using Pet4U.API.Extensions;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.GetUrlPetPhoto;
using Pet4U.Application.UseCases.RemovePetPhotoFile;

namespace Pet4U.API.Controllers;

public class FileController : ApplicationController
{
  private readonly IMinioClient _minioClient;
public FileController(IMinioClient minioClient)
{
  _minioClient = minioClient;
}


  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetUrlPetPhoto
  ([FromRoute] string id, 
  [FromServices] IGetUrlPetPhotoHandler handler,
  CancellationToken cancellationToken = default)
  {
    var command = GetUrlFileCommand.ToCommand("photos",id);
    var result = await handler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }
 
  [HttpPost("photos")]
  public async Task<IActionResult> CreateFile
  (IFormFile file, 
  [FromServices] IAddPetPhotoHandler addPetPhotoHandler,
  CancellationToken cancellationToken = default)
  {
   await using var stream = file.OpenReadStream();
   var path = Guid.NewGuid().ToString();

   var command = UploadFileCommand.ToCommand(stream,"photos", path);

    var result = await addPetPhotoHandler.HandleAsync(command, cancellationToken);

    if(result.IsFailure)
      return result.Error.ToResponse();

    return Ok(result.Value);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> RemovePhoto
  ([FromRoute] string id,
  [FromServices] IRemovePetPhotoHandler handler,
  CancellationToken cancellationToken = default)
  {
    var command = RemoveFileCommand.ToCommand("photos",id, string.Empty);
    var result = await handler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }
}

