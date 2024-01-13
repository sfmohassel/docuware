using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Adapters.Users.EFTypeConfigurations;

public class UserRoleTypeConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("user_roles")
      .HasKey(a => new { a.UserId, a.Role });

    builder.HasOne(a => a.User)
      .WithMany(a => a.UserRoles)
      .HasForeignKey(a => a.UserId)
      .IsRequired();

    builder.Property(a => a.UserId).HasColumnName("user_id");
    builder.Property(a => a.Role).HasColumnName("role").HasConversion<string>();
  }
}