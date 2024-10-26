using Pet4U.Application.FileProdiver;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.AddPetPhoto;

public interface IAddPetPhotoHandler
{
  Task<Result<string>> HandleAsync(UploadFileCommand command,CancellationToken cancellationToken);
}