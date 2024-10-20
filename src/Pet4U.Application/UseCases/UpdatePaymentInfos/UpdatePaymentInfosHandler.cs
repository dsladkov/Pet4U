using Microsoft.Extensions.Logging;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.UseCases.UpdatePaymentInfos;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdatePaymentInfos;


public class UpdatePaymentInfosHandler : IUpdatePaymentInfosHandler
{
  private readonly IVolunteersRepository _volunteerRepository;
    private readonly ILogger<UpdatePaymentInfosHandler> _logger;

    public UpdatePaymentInfosHandler(IVolunteersRepository volunteerRepository, ILogger<UpdatePaymentInfosHandler> logger)
  {
    _volunteerRepository = volunteerRepository;
    _logger = logger;
  }
  public async Task<Result<Guid>> HandleAsync
  (
    UpdatePaymentInfosCommand command,
    CancellationToken cancellationToken
  )
  {
    var volunteerResult = await _volunteerRepository.GetByIdAsync(VolunteerId.Create(command.VolunteerId), cancellationToken);
    if(volunteerResult.IsFailure)
      return volunteerResult.Error;

   var paymentInfos =  (from item in command.PaymentInfoDtos
                         let sn = PaymentInfo.Create(item.Title, item.Description )
                         select sn);
     
   volunteerResult?.Value?.UpdatePaymentInfos(new PaymentInfos(paymentInfos));

    var volunteerUpdated = await _volunteerRepository.Save(volunteerResult.Value,cancellationToken);
    
    _logger.LogInformation("PaymentInfos of Volunteer with id {0} have been updated", volunteerResult.Value.Id);
    return volunteerUpdated;
  }
}