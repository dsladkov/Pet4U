using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                value => VolunteerId.Create(value)
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
            .WithOne(v => v.Volunteer)
            .HasForeignKey("Volunteeer_Id")
            .IsRequired(false);
            //.HasForeignKey("pet_id")
            //.OnDelete(DeleteBehavior.NoAction);
            //.HasPrincipalKey("pet_id")
            //.HasForeignKey("volunteer_id");

            builder.HasMany(v => v.SocialNetworks)
                   .WithMany();

            builder.HasMany(m => m.PaymentInfos)
                   .WithOne(m => m.Volunteer)
                   .HasForeignKey("volunteer_id")
                   .IsRequired(false);
            //.HasForeignKey("payment_info_id");
            //.HasForeignKey("volunteer_id");
        }
    }
}