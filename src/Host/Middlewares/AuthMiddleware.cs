using System.Security.Claims;
using Application.Users.UseCases;
using Host.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Host.Middlewares;

public class AuthMiddleware(RequestDelegate next)
{
  private const string PREFIX = "Bearer ";

  public async Task InvokeAsync(HttpContext context, UserUseCases userUseCases)
  {
    var authorizationHeader = context.Request.Headers.Authorization.ToString();

    if (authorizationHeader.StartsWith(PREFIX))
    {
      var token = authorizationHeader[PREFIX.Length..];
      var userId = JWT.Parse(token);
      if (userId.HasValue)
      {
        var user = await userUseCases.GetById(userId.Value);
        var claims = new List<Claim>(new[]
        {
          new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
          new Claim(ClaimTypes.Name, user.Email)
        });
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.ToString()))
          .ToList());
        context.User = new ClaimsPrincipal(
          new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme));
        context.SetExecutingUser(user);
      }
    }

    await next(context);
  }
}