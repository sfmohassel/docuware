using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Adapters.Users.EFTypeConfigurations;

public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users")
      .HasKey(a => a.Id);

    builder.Property(a => a.Id).HasColumnName("id").IsRequired();
    builder.Property(a => a.PublicId).HasColumnName("pid").IsRequired();

    builder.HasMany(a => a.UserRoles)
      .WithOne(a => a.User)
      .HasForeignKey(a => a.UserId)
      .IsRequired();

    builder.Property(a => a.Email).HasColumnName("email").HasMaxLength(64).IsRequired();
    builder.Property(a => a.Password).HasColumnName("password").HasMaxLength(255).IsRequired();
  }
}