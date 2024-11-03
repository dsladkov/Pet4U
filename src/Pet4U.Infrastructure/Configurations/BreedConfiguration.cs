using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;
using Pet4U.Domain.SpeciesManagement.ValueObject;

namespace Pet4U.Infrastructure 
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("breed");
            builder.HasKey(b => b.Id);

            builder.Property(s => s.Id)
                   .HasConversion(
                     Id => Id,
                     value => value
                   );

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(b => b.Description)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property<bool>("_isDeleted")
                   .UsePropertyAccessMode(PropertyAccessMode.Field)
                   .HasColumnName("is_deleted");
        }
    }
}