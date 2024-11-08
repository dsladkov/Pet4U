using Microsoft.Extensions.Logging;
using Pet4U.Application.Database;
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

    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentInfosHandler(IVolunteersRepository volunteerRepository, IUnitOfWork unitOfWork,ILogger<UpdatePaymentInfosHandler> logger)
      {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
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
                        let sn = PaymentInfo.Create(item.title, item.description )
                        select sn.Value);
     
    volunteerResult?.Value?.UpdatePaymentInfos(new PaymentInfos(paymentInfos));

    
    await _unitOfWork.SaveChangesAsync(cancellationToken);
    
    _logger.LogInformation("PaymentInfos of Volunteer {FirstName} {LastName} with id {Id} have been updated",
      volunteerResult.Value.FullName.FirstName,
      volunteerResult.Value.FullName.LastName, 
      volunteerResult.Value.Id);

    return volunteerResult.Value;
  }
}