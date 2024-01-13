using Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Adapters.Events.EFTypeConfigurations;

public class RegistrationTypeConfiguration : IEntityTypeConfiguration<Registration>
{
  public void Configure(EntityTypeBuilder<Registration> builder)
  {
    builder.ToTable("registrations")
      .HasKey(a => a.Id);

    builder.Property(a => a.Id).HasColumnName("id").IsRequired();
    builder.Property(a => a.PublicId).HasColumnName("pid").IsRequired();

    builder.Property(a => a.EventId).HasColumnName("event_id").IsRequired();
    builder.Property(a => a.RegisteredAt).HasColumnName("registered_at").IsRequired();
    builder.Property(a => a.Name).HasColumnName("name").HasMaxLength(64).IsRequired();
    builder.Property(a => a.Phone).HasColumnName("phone").HasMaxLength(32).IsRequired(false);
    builder.Property(a => a.Email).HasColumnName("email").HasMaxLength(32).IsRequired(false);
  }
}