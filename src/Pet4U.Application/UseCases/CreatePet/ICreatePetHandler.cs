using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.CreatePet;

public interface ICreatePetHandler
{
  Task<Result<Guid>> HandleAsync
  (
    CreatePetCommand command,
    CancellationToken cancellationToken
  );
}