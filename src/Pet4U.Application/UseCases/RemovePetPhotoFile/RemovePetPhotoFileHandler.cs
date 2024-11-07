using Microsoft.Extensions.Logging;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Application.UseCases.RemovePetPhotoFile;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.RemovePetPhotoFile;

public class RemovePetPhotohandler : IRemovePetPhotoHandler
{
  private readonly IFileProvider _fileProvider;
    private readonly ILogger<RemovePetPhotohandler> _logger;

  public RemovePetPhotohandler(IFileProvider fileProvider, ILogger<RemovePetPhotohandler> logger)
  {
    _fileProvider = fileProvider;
    _logger = logger;
  }


  public async Task<Result> HandleAsync
  (
    RemoveFileCommand command,
    CancellationToken cancellationToken
  )
  {
    var file = new RemoveFileData(command.BucketName, command.Id,null);
    return await _fileProvider.RemoveFileAsync(file, cancellationToken);
  }
}
