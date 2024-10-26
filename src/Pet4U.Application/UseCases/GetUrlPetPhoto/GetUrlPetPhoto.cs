using Microsoft.Extensions.Logging;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.GetUrlPetPhoto;

public class GetUrlPetPhotoHandler : IGetUrlPetPhotoHandler
{
  private readonly IFileProvider _fileProvider;
    private readonly ILogger<GetUrlPetPhotoHandler> _logger;

  public GetUrlPetPhotoHandler(IFileProvider fileProvider, ILogger<GetUrlPetPhotoHandler> logger)
  {
    _fileProvider = fileProvider;
    _logger = logger;
  }


  public async Task<Result> HandleAsync
  (
    GetUrlFileCommand command,
    CancellationToken cancellationToken
  )
  {
    var fileData = new UrlFileData(command.BucketName, command.ObjectName, null);
    return await _fileProvider.GetUrlFileAsync(fileData, cancellationToken);
  }
}
