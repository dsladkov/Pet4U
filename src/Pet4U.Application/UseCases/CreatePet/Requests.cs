
using System.Data;
using FluentValidation;
using Pet4U.Application.Validation;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.Shared.ValueObjects;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;
using Pet4U.Application.UseCases.Shared;

namespace Pet4U.Application.UseCases.CreateVolunteer;
public record CreatePetCommand
(
  Guid id,
  string Nickname,
  string Species,
  string Description,
  string Breed,
  string Color,
  string Health,
  string Address,
  double Weight,
  double Height,
  string Phone,
  bool IsNeutered,
  DateOnly? Birthday,
  bool IsVaccinated,
  Status Status,
  DateOnly CreateDate
);
public record CreatePetRequest
(
  string nickname, 
  string species, 
  string description,
  string breed,
  string color,
  string health,
  string address,
  double weight,
  double height,
  string phone,
  bool isNeutered,
  //DateOnly? birthDay,
  bool isVaccinated,
  Status status
)
{

  string Nickname = nickname;
  string Species = species;
  string Description = description;
  string Breed = breed;
  string Color = color;
  string Health = health;
  string Address = address;
  double Weight = weight;
  double Height = height;
  string Phone = phone;
  bool IsNeutered = isNeutered;
  //DateOnly? Birthday = birthDay;
  bool IsVaccinated = isVaccinated;
  Status Status = status;
  //DateOnly CreateDate = DateOnly.FromDateTime(DateTime.Today);
  public CreatePetCommand ToCommand(Guid id) => new(id,Nickname, Species, Description, Breed, Color, Health, Address,Weight,Height,Phone,IsNeutered,DateOnly.FromDateTime(DateTime.Today),IsVaccinated,Status,DateOnly.FromDateTime(DateTime.Today));
}

// public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
// {
//   public CreatePetRequestValidator()
//   {
//     RuleFor(p => p.nickname).NotEmpty();
//     RuleFor(p => p.species).NotNull().NotEmpty();
//     RuleFor(p => p.description).NotNull().NotEmpty();
//     RuleFor(p => p.breed).NotNull().NotEmpty();
//     RuleFor(p => p.color).NotNull().NotEmpty();
//     RuleFor(p => p.health).NotNull().NotEmpty();
//     RuleFor(p => p.address).NotNull().NotEmpty();
//     RuleFor(p => p.weight).NotNull().NotEmpty();
//     RuleFor(p => p.phone).NotNull().NotEmpty();
//     RuleFor(p => p.breed).NotNull().NotEmpty();
//     RuleFor(p => p.isNeutered).NotNull();
//  RuleFor(c => c.nickname).

//     RuleFor(p => p.birthDay).EmailAddress();

//     RuleFor(c => c.Description).MustBeValueObject(Description.Create);
//     RuleFor(c => c.Phone).MustBeValueObject(Phone.Create);
   
//   }
// }


