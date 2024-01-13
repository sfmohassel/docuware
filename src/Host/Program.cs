using Application.Events.UseCases;
using Application.Users.UseCases;
using Domain.Entities.Users;
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
var services = builder.Services;
services.AddEndpointsApiExplorer().AddSwaggerGen();

var databaseConfig = builder.Configuration.GetSection("Database").Get<DatabaseConfig>()!;
var adminConfig = builder.Configuration.GetSection("Admin").Get<AdminConfig>()!;

services.AddDbContext<EFContext>(o =>
{
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
  o.UseNpgsql(connectionStringBuilder.ConnectionString);
});

services
  .AddSingleton<IClock, Clock>()
  .AddSingleton<IPasswordHasher, PasswordHasher>()
  .AddSingleton<ITransactionFactory, TransactionFactory>()
  .AddSingleton<UserService>()
  .AddSingleton<AuthUseCases>()
  .AddSingleton<UserUseCases>()
  .AddSingleton<EventUseCases>()
  .AddSingleton<IUserRepository, UserRepository>()
  .AddSingleton<IEventRepository, EventRepository>()
  .AddSingleton<IRegistrationRepository, RegistrationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHealthChecks("/");
app.MapControllers();

var userRepository = app.Services.GetRequiredService<IUserRepository>();
var passwordHasher = app.Services.GetRequiredService<IPasswordHasher>();
var admin = await userRepository.FindByEmail(adminConfig.Email);
if (admin == null)
{
  admin = new User(adminConfig.Email, await passwordHasher.Hash(adminConfig.Password));
  await userRepository.Save(admin);
}

app.Run();