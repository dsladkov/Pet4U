using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pet4U.Domain;
using Pet4U.Domain.Modules;
using Pet4U.Domain.Shared;

namespace Pet4U.Infrastructure 
{
    public class VolunteerConfiguration : IEntityTypeConfiguration<Pet4U.Domain.Modules.Volunteer>
    {
        public void Configure(EntityTypeBuilder<Volunteer> builder)
        {
            builder.ToTable("volunteers");
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
              .HasConversion(
                Id => Id.Value,
                value => VolunteerId.GetNew(value)
              );

            builder.ComplexProperty(v => v.FullName, ba =>
            {
                ba.Property(p => p.FirstName)
                    .HasColumnName("first_name");
                ba.Property(p => p.LastName)
                    .HasColumnName("last_name");
                ba.Property(p => p.MiddleName)
                    .HasColumnName("midle_name");
            });  

            builder.Property(v => v.Email)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

            builder.Property(v => v.Experience);

            builder.Property(v => v.Phone)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

            builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_Id");

            builder.OwnsOne(v => v.SocialNetworks, d => 
            {
              d.ToJson();
              d.OwnsMany(d => d.Data);
            });

            builder.OwnsOne(v => v.PaymentInfos, d => 
            {
              d.ToJson();
              d.OwnsMany(d => d.Data);
            });
        }
    }
}