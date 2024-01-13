using Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Adapters.Events.EFTypeConfigurations;

public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
{
  public void Configure(EntityTypeBuilder<Event> builder)
  {
    builder.ToTable("events")
      .HasKey(a => a.Id);

    builder.Property(a => a.Id).HasColumnName("id").IsRequired();
    builder.Property(a => a.PublicId).HasColumnName("pid").IsRequired();

    builder.Property(a => a.CreatorId).HasColumnName("creator_id").IsRequired();
    builder.Property(a => a.Name).HasColumnName("name").HasMaxLength(64).IsRequired();
    builder.Property(a => a.Start).HasColumnName("start").IsRequired();
    builder.Property(a => a.End).HasColumnName("end").IsRequired();
    builder.Property(a => a.Description).HasColumnName("description").HasMaxLength(1024)
      .IsRequired(false);

    var locationBuilder = builder.OwnsOne(a => a.Location);
    locationBuilder.Property(a => a.Country).HasColumnName("country").IsFixedLength()
      .HasMaxLength(2).IsRequired(false);
    locationBuilder.Property(a => a.City).HasColumnName("city").HasMaxLength(64).IsRequired(false);
    locationBuilder.Property(a => a.PostalCode).HasColumnName("postcode").HasMaxLength(32)
      .IsRequired(false);
    locationBuilder.Property(a => a.StreetAndHouse).HasColumnName("streethouse").HasMaxLength(128)
      .IsRequired(false);
  }
}