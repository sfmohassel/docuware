using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Users.EFTypeConfigurations;

public class UserRoleTypeConfiguration : IEntityTypeConfiguration<User.UserRole>
{
  public void Configure(EntityTypeBuilder<User.UserRole> builder)
  {
    builder.ToTable("user_roles")
      .HasKey(a => new { a.UserId, a.Role });

    builder.HasOne<User>()
      .WithMany(a => a.UserRoles)
      .HasForeignKey(a => a.UserId)
      .IsRequired();

    builder.Property(a => a.UserId).HasColumnName("user_id");
    builder.Property(a => a.Role).HasColumnName("role").HasConversion<string>();
  }
}