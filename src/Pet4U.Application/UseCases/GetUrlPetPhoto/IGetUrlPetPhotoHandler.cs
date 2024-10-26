using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.GetUrlPetPhoto;

public interface IGetUrlPetPhotoHandler
{
  Task<Result> HandleAsync
  (
    GetUrlFileCommand command,
    CancellationToken cancellationToken
  );
}