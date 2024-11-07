using Microsoft.Extensions.Logging;
using Pet4U.Application.FileProdiver;
using Pet4U.Application.UseCases.AddPetPhoto;
using Pet4U.Application.UseCases.Providers;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.AddPetsMediaFiles;

public class UploadMediaHandler : IUploadMediaHandler
{
  private const int MAX_CONCURRENT_REQUESTS = 10;
  private readonly IFileProvider _fileProvider;
    private readonly ILogger<UploadMediaHandler> _logger;

  public UploadMediaHandler(IFileProvider fileProvider, ILogger<UploadMediaHandler> logger)
  {
    _fileProvider = fileProvider;
    _logger = logger;
  }


  public async Task<Result<IReadOnlyList<string>>> HandleAsync
  (
    UploadFilesCommand command,
    CancellationToken cancellationToken
  )
  {
    List<Result<string>> results = [];
    try
    {
        IEnumerable<FileData> filesData = command.uploadFilesCommand
                .Select(file => new FileData(file.Stream, file.BucketName, Guid.NewGuid().ToString() + file.ObjectName.Substring(file.ObjectName.IndexOf('.') - 1)));

        var result = await _fileProvider.UploadFiles(filesData, cancellationToken);

        return result.Value.Select(r => r).ToList();
     }
    catch (Exception ex)
    {
      _logger.LogError(ex, ex.Message);
      return Error.Failure("file.upload", "fail to upload file in minio");
    }
  }
    //public async Task<Result<IReadOnlyList<string>>> HandleAsync
    //(
    //  UploadFilesCommand command,
    //  CancellationToken cancellationToken
    //)
    //{
    //    List<Result<string>> results = [];
    //    var semaphoreSlim = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS);
    //    try
    //    {
    //        await semaphoreSlim.WaitAsync(cancellationToken);

    //        await Parallel.ForEachAsync(command.uploadFilesCommand, async (file, cancellationToken) => {

    //            var fileData = new FileData(file.Stream, file.BucketName, Guid.NewGuid().ToString() + file.ObjectName.Substring(file.ObjectName.IndexOf('.') - 1));
    //            var result = await _fileProvider.UploadFileWithSemaphoreAsync(fileData, semaphoreSlim, cancellationToken);
    //            results.Add(result);
    //        });

    //        return results.Select(r => r.Value).ToList();
    //        //return Result<IReadOnlyList<string>>.Success(results.Select(r => r.Value).ToList().AsReadOnly());
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.Message);
    //        return Error.Failure("file.upload", "fail to upload file in minio");
    //    }
    //    finally
    //    {
    //        _ = semaphoreSlim.Release(MAX_CONCURRENT_REQUESTS);
    //    }


    //}
}
