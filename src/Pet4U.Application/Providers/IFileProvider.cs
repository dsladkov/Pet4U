using Pet4U.Application.FileProdiver;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.Providers;

public interface IFileProvider
{
  Task<Result<string>> UploadFileAsync(FileData fileData,CancellationToken cancellationToken = default);
}
