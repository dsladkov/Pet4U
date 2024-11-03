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

   foreach(var f in formFiles.ToArray())
   {
    f.OpenReadStream();
   }

   var command = UploadFileCommand.ToCommand(stream,"photos",path);

    var result = await addPetPhotoHandler.HandleAsync(command, cancellationToken);

    if(result.IsFailure)
      return result.Error.ToResponse();

    return Ok(result.Value);
  }


  [HttpPost("new-media")]
  public async Task<ActionResult> UploadFilesNew
  ([FromServices] IUploadMediaHandler uploadMediaHandler,
  //[FromServices] IFormFileCollectionProcessor processor,
  IFormFileCollection files,
  CancellationToken cancellationToken = default)
  {
    await using var processor = new FormFileCollectionProcessor();
    var command = processor.Process(files);
    var result = await uploadMediaHandler.HandleAsync(command, cancellationToken);
    return result.ToResponse();
  }

  [HttpPost("media")]
  public async Task<ActionResult> UploadFiles
  ([FromServices] IUploadMediaHandler uploadMediaHandler,
  IFormFileCollection files,
  CancellationToken cancellationToken = default)
  {
   
   List<Result<string[]>> results = [];

   List<Stream> streams = new List<Stream>();
   try
   {
      Parallel.ForEach(files, (file) => {

      var stream = file.OpenReadStream();

      streams.Add(stream);
      });

      var path = Guid.NewGuid().ToString();
      //var command = UploadFileCommand.ToCommand(stream,"photos", path);
      var command = UploadFilesCommand.ToCommand(streams.Select(s => new UploadFileCommand(s, "photos",path)));
      var result = await uploadMediaHandler.HandleAsync(command, cancellationToken);
      results.Add(result);
      
      return results.First().ToResponse();
   }
   catch (Exception ex)
   {
    _logger.LogError(ex, ex.Message);
   }
   finally
   {
    streams.ForEach(s => s.DisposeAsync());
   }

    if(results.Any(r =>r .IsFailure))
      return results!.Where(r => r.IsFailure)!.FirstOrDefault()!.Error.ToResponse();

   return Ok(results.FirstOrDefault()!.Value);
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

