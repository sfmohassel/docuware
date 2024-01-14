using Application.API.Users.DTO;
using Application.Users.UseCases;
using Host.Security;
using Infra.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthUseCases authUseCases, JwtConfig jwtConfig)
{
  [HttpPost("login")]
  public async Task<LoginOutput> Login([FromBody] LoginInput input)
  {
    var user = await authUseCases.Login(input);
    var token = JWT.Generate(user, jwtConfig);

    return new LoginOutput
    {
      Token = token,
      Roles = user.Roles
    };
  }
}