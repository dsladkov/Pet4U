using Pet4U.Application.UseCases.RemovePetPhotoFile;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.RemovePetPhotoFile;

public interface IRemovePetPhotoHandler
{
  Task<Result> HandleAsync
  (
    RemoveFileCommand command,
    CancellationToken cancellationToken
  );
}