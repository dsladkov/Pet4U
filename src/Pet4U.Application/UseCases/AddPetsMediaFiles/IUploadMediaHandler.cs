using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.AddPetsMediaFiles;

public interface IUploadMediaHandler
{
  Task<Result<IReadOnlyList<string>>> HandleAsync
  (
    UploadFilesCommand command,
    CancellationToken cancellationToken
  );
}