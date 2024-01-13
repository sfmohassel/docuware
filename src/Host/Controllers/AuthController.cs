using Application.API.Users.DTO;
using Application.Users.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(AuthUseCases authUseCases) : ControllerBase
{
  [HttpPost("login")]
  public Task<LoginOutput> login([FromBody] LoginInput input)
  {
    return authUseCases.Login(input);
  }
}