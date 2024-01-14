using System.Text.Json;
using System.Text.Json.Serialization;
using Application.API.Users.Models;
using Application.Events.UseCases;
using Application.Users.UseCases;
using Domain.Events.Repositories;
using Domain.Ports;
using Domain.Users.Ports;
using Domain.Users.Repositories;
using Domain.Users.Services;
using Host.Middlewares;
using Host.Security;
using Infra.Adapters;
using Infra.Configuration;
using Infra.Events.Repositories;
using Infra.Users.Adapters;
using Infra.Users.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
  builder.Configuration.AddJsonFile("appsettings.Local.json", optional: false,
    reloadOnChange: true);
}

var databaseConfig = builder.Configuration.GetSection("Database").Get<DatabaseConfig>()!;
var adminConfig = builder.Configuration.GetSection("Admin").Get<AdminConfig>()!;
var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfig>()!;


var services = builder.Services;
services.AddEndpointsApiExplorer().AddSwaggerGen().AddHealthChecks();
services.AddHttpContextAccessor().AddControllers()
  .AddJsonOptions(o =>
  {
    o.JsonSerializerOptions.AllowTrailingCommas = true;
    o.JsonSerializerOptions.IncludeFields = true;
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    o.JsonSerializerOptions.WriteIndented = true;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

services.AddAuthorization(o =>
{
  o.AddPolicy("Authenticated", policy =>
  {
    policy.Requirements.Add(new RoleRequirement());
  });
  o.AddPolicy("EventCreator", policy =>
  {
    policy.Requirements.Add(new RoleRequirement(Role.EventCreator));
  });
});

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

services.AddDbContext<EFContext>(o => { o.UseNpgsql(connectionStringBuilder.ConnectionString); });

services
  .AddScoped<IAuthorizationHandler, RoleAccessHandler>()
  .AddSingleton(jwtConfig)
  .AddSingleton<JWT>()
  .AddSingleton<IClock, Clock>()
  .AddSingleton<IPasswordHasher, PasswordHasher>()
  .AddScoped<ITransactionFactory, TransactionFactory>()
  .AddScoped<IUserService, UserService>()
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
app.UseAuthentication();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthorization();
app.MapControllers();

// migrate database
Migrator.Migrate(connectionStringBuilder.ConnectionString);

// ensure one admin user exists in database
using (var scope = app.Services.CreateScope())
{
  var userUseCases = scope.ServiceProvider.GetRequiredService<UserUseCases>();
  await userUseCases.SeedAdmin(adminConfig.Email, adminConfig.Password);
}

app.Run();