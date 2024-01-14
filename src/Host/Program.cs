using Application.Events.UseCases;
using Application.Users.UseCases;
using Domain.Ports;
using Domain.Ports.Events.Repositories;
using Domain.Ports.Users;
using Domain.Ports.Users.Repositories;
using Domain.Ports.Users.Services;
using Infra.Adapters;
using Infra.Adapters.Configuration;
using Infra.Adapters.Events.Repositories;
using Infra.Adapters.Users;
using Infra.Adapters.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
  builder.Configuration.AddJsonFile("appsettings.Local.json", optional: false, reloadOnChange: true);
}
var services = builder.Services;
services.AddEndpointsApiExplorer().AddSwaggerGen().AddHealthChecks();
services.AddControllers();

var databaseConfig = builder.Configuration.GetSection("Database").Get<DatabaseConfig>()!;
var adminConfig = builder.Configuration.GetSection("Admin").Get<AdminConfig>()!;
var connectionStringBuilder = new NpgsqlConnectionStringBuilder
{
  Username = databaseConfig.User,
  Password = databaseConfig.Password,
  Host = databaseConfig.IP,
  Port = databaseConfig.Port,
  Pooling = true,
  MaxPoolSize = 10,
  MinPoolSize = 1,
  Database = databaseConfig.Name
};

services.AddDbContext<EFContext>(o =>
{
  o.UseNpgsql(connectionStringBuilder.ConnectionString);
});

services
  .AddSingleton<IClock, Clock>()
  .AddSingleton<IPasswordHasher, PasswordHasher>()
  .AddScoped<ITransactionFactory, TransactionFactory>()
  .AddScoped<UserService>()
  .AddScoped<AuthUseCases>()
  .AddScoped<UserUseCases>()
  .AddScoped<EventUseCases>()
  .AddScoped<IUserRepository, UserRepository>()
  .AddScoped<IEventRepository, EventRepository>()
  .AddScoped<IRegistrationRepository, RegistrationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHealthChecks("/");
app.MapControllers();

Migrator.Migrate(connectionStringBuilder.ConnectionString);

using (var scope = app.Services.CreateScope())
{
  var userUseCases = scope.ServiceProvider.GetRequiredService<UserUseCases>();
  await userUseCases.SeedAdmin(adminConfig.Email, adminConfig.Password);
}

app.Run();