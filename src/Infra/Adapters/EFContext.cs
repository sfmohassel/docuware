using Domain.Events.Entities;
using Domain.Users.Entities;
using Infra.Users.EFTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters;

public class EFContext(DbContextOptions<EFContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; private set; }
  public DbSet<Event> Events { get; private set; }
  public DbSet<Registration> Registrations { get; private set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserTypeConfiguration).Assembly);
  }
}