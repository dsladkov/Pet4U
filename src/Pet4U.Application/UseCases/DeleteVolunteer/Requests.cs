
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Application.UseCases.DeleteVolunteer;


public record DeleteVolunteerCommand (Guid Id);
public record DeleteVolunteerRequest(Guid Id)
{
  public DeleteVolunteerCommand ToCommand() => new (Id);
}

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
{
  public DeleteVolunteerRequestValidator() => RuleFor(r => r.Id).NotEmpty().WithError(Errors.General.ValueIsRequired("VolunteerId"));
}

