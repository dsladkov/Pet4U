using Pet4U.Domain.Shared;

namespace Pet4U.Application.UseCases.UpdatePaymentInfos;

public interface IUpdatePaymentInfosHandler
{
  Task<Result<Guid>> HandleAsync
  (
    UpdatePaymentInfosCommand command,
    CancellationToken cancellationToken
  );
}