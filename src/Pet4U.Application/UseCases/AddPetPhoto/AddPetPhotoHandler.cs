using Microsoft.Extensions.Logging;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.AddPetPhoto;

public class AddPetPhotoHandler : IAddPetPhotoHandler
{
  private readonly IFileProvider _fileProvider;
    private readonly ILogger<AddPetPhotoHandler> _logger;

  public AddPetPhotoHandler(IFileProvider fileProvider, ILogger<AddPetPhotoHandler> logger)
  {
    _fileProvider = fileProvider;
    _logger = logger;
  }


  public async Task<Result<string>> HandleAsync
  (
    UploadFileCommand command,
    CancellationToken cancellationToken
  )
  {
    var fileData = new FileData(command.Stream, command.BucketName, command.ObjectName);
    return await _fileProvider.UploadFileAsync(fileData, cancellationToken);
  }
}