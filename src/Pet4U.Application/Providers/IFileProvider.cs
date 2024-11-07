using Pet4U.Application.FileProdiver;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.Providers;

public interface IFileProvider
{
  Task<Result<string>> UploadFileAsync(
      FileData fileData,
      CancellationToken cancellationToken = default);

    Task<Result<IReadOnlyCollection<string>>> UploadFiles
        (IEnumerable<FileData> files,
        CancellationToken cancellationToken = default);
    Task<Result<string>> UploadFileWithSemaphoreAsync(
    FileData fileData,
    SemaphoreSlim semaphore,
    CancellationToken cancellationToken = default);
  Task<Result<string>> GetUrlFileAsync(UrlFileData file, CancellationToken cancellationToken = default);
  Task<Result> RemoveFileAsync(RemoveFileData file, CancellationToken cancellationToken = default);
}
