using Application.API.Users.Models;

namespace Application.API.Users.DTO;

public class LoginOutput
{
  public string Token { get; set; }
  public IList<Role> Roles { get; set; }
}