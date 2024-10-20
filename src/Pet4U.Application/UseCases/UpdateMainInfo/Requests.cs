
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;
using Pet4U.Application.UseCases.Shared;

namespace Pet4U.Application.UseCases.UpdateMainInfo;


public record UpdateMainInfoDto(FullNameDto FullNameDto, string Email,string Description, int Experience ,string Phone);
public record UpdateMainInfoVolunteerRequest
(
  Guid VolunteerId,
  UpdateMainInfoDto Dto
)
{
  public UpdateMainInfoVolunteerCommand ToCommand() => 
  new( 
    VolunteerId, 
    Dto.FullNameDto, 
    Dto.Email, 
    Dto.Description, 
    Dto.Experience, 
    Dto.Phone
    );
}

public record UpdateMainInfoVolunteerCommand
( Guid VolunteerId,
  FullNameDto FullNameDto,
  string Email,
  string Description,
  int Experience,
  string Phone
);


public class UpdateMainInfoVolunteerRequestValidator : AbstractValidator<UpdateMainInfoVolunteerRequest>
{
  public UpdateMainInfoVolunteerRequestValidator()
  {
    RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
    RuleFor(r => r.Dto.FullNameDto).MustBeValueObject(x => FullName.Create(x.FirstName, x.LastName, x.MiddleName));
    RuleFor(r => r.Dto.Email).NotEmpty().EmailAddress();
    RuleFor(r => r.Dto.Experience).NotEmpty().WithError(Errors.General.ValueIsRequired("Experience"));
    RuleFor(r => r.Dto.Description).NotEmpty().MustBeValueObject(Description.Create);
    RuleFor(r => r.Dto.Phone).NotEmpty().MustBeValueObject(Phone.Create);
  }
}