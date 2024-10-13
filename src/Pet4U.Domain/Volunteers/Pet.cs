using System;
using System.Collections.Generic;
using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;

namespace Pet4U.Domain.Modules
{
  public class Pet : Entity<PetId>
  {
    private List<PetPhoto> _petPhoto = [];

    private Pet(PetId id) : base(id){}

    public Pet
    (
      PetId id,
      PetData petData,
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
      DateOnly? birthDay,
      bool isVaccinated,
      Status status,
      DateOnly createDate
    ) : base(id)
    {
      PetData = petData;
      Nickname = nickname;
      Species = species;
      Description = description;
      Breed = breed;
      Color = color;
      Health = health;
      Address = address;
      Weight = weight;
      Height = height;
      Phone = phone;
      IsNeutered = isNeutered;
      Birthday = birthDay;
      IsVaccinated = isVaccinated;
      Status = status;
      CreateDate = createDate;
    }

    public PetData PetData { get; private set; } = null!;
    public string Nickname { get; private set; } = null!;
    public string Species { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Breed {get; private set;} = null!;
    public string Color { get; private set; } = null!;
    public string Health { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public string Phone { get; private set; } = null!;
    public bool? IsNeutered { get; private set; }
    public DateOnly? Birthday { get; private set; }
    public bool IsVaccinated { get; private set; }
    public Status Status { get; private set; }
    public DateOnly CreateDate { get; private set; }
    
    public IReadOnlyCollection<PetPhoto> PetPhotos => _petPhoto;

    public void AddPetPhoto(PetPhoto petPhoto) => _petPhoto.Add(petPhoto);
  }
}