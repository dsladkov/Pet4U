using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using Pet4U.API.Extensions;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;

namespace Pet4U.API.Controllers;

public class FileController : ApplicationController
{
  private readonly IMinioClient _minioClient;
public FileController(IMinioClient minioClient)
{
  _minioClient = minioClient;
}

 
  [HttpPost("photos")]
  public async Task<IActionResult> CreateFile(IFormFile file, [FromServices] IAddPetPhotoHandler addPetPhotoHandler,CancellationToken cancellationToken = default)
  {
   await using var stream = file.OpenReadStream();
   var path = Guid.NewGuid().ToString();

   var command = UploadFileCommand.ToCommand(stream,"photo", path);

    var result = await addPetPhotoHandler.HandleAsync(command, cancellationToken);

    if(result.IsFailure)
      return result.Error.ToResponse();

    return Ok(result.Value);
  }
}

