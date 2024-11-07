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
public record AddPetPhotoCommand
(
  Guid volunteerId,
  Guid petId,
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
public record AddPetPhotoRequest
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
  public AddPetPhotoCommand ToCommand(Guid volunteerId, Guid petId) => new(volunteerId, petId, Nickname, Species, Description, Breed, Color, Health, Address,Weight,Height,Phone,IsNeutered,DateOnly.FromDateTime(DateTime.Today),IsVaccinated,Status,DateOnly.FromDateTime(DateTime.Today));
}