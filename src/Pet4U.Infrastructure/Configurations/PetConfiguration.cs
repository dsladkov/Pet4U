using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet4U.Domain.Ids;
using Pet4U.Domain.ValueObjects;
using Pet4U.Domain.Volunteers;

namespace Pet4U.Infrastructure 
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable("pets");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
              .HasConversion(
                Id => Id.Value,
                value => PetId.Create(value)
              );

            builder.Property(v => v.Nickname)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Species)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(v => v.Breed)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);


            builder.Property(v => v.Color)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Health)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Address)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Weight)
            .IsRequired();

            builder.Property(v => v.Height)
            .IsRequired();

            builder.Property(v => v.Phone)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.IsNeutered)
            .IsRequired(false);

            builder.Property(v => v.Birthday)
            .IsRequired(false);

            builder.Property(v => v.IsVaccinated)
            .IsRequired(true);

            builder.Property(v => v.CreateDate)
            .IsRequired(true);

            builder.ComplexProperty(p => p.PetData, pdb =>
            {
              pdb.Property(d => d.SpeciesId)
              .HasConversion(
                value => value.Value,
                value => SpeciesId.Create(value)
              );
              pdb.Property(d => d.BreedId)
              .IsRequired();
            });

            // builder.ComplexProperty(p => p.SpeciesId, pb =>
            // {
            //     pb.Property(p => p.Value)
            //         .HasColumnName("species_id");
            // });

            // builder.Property(p => p.BreedId);
        }
    }
}