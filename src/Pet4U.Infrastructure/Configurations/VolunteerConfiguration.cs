using System.Data;
using System.Reflection.Metadata;
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
                value => VolunteerId.Create(value)
              ).HasColumnName("volunteer_id");

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

            builder.ComplexProperty(v => v.Description, db =>
            {
              db.Property(d => d.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH)
                .HasColumnName("description");
            });

            builder.Property(v => v.Experience);

            builder.ComplexProperty(v => v.Phone, bp =>
            {
              bp.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("phone");
            });

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