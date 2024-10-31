
using System.Data;
using FluentValidation;
using Pet4U.Application.UseCases.CreateVolunteer;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.UpdatePaymentInfos;

  // public record PaymentInfoDto//(string title, string description)
  // {
  //   public string Title { get; set; } = null!; //= title;
  //   public string Description { get; set; } = null!;
  // }

public record UpdatePaymentInfosCommand
( Guid VolunteerId,
  IReadOnlyCollection<PaymentInfoDto> PaymentInfoDtos
);

public record UpdatePaymentInfoDto(IReadOnlyCollection<PaymentInfoDto> paymentInfoDtos);
public record UpdatePaymentInfosRequest(Guid Id,UpdatePaymentInfoDto Dto)
{
  public UpdatePaymentInfosCommand ToCommand() => new(Id, Dto.paymentInfoDtos );
}

public class UpdatePaymentInfosRequestValidator : AbstractValidator<UpdatePaymentInfosRequest>
{
  public UpdatePaymentInfosRequestValidator()
  {
    RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleForEach(r => r.Dto.paymentInfoDtos)
              .NotNull();
  }
}
