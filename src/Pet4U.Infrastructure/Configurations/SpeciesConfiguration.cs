using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet4U.Domain.Shared;
using Pet4U.Domain.Shared.Ids;
using Pet4U.Domain.SpeciesManagement.AgregateRoot;

namespace Pet4U.Infrastructure 
{
    public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("species");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasConversion(
                     Id => Id.Value,
                     value => SpeciesId.Create(value)
                   );

            builder.Property(s => s.Title)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(s => s.Description)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.HasMany(s => s.Breeds)
                   .WithOne()
                   .HasForeignKey("species_id");
        }
    }
}