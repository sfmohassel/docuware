using Application.API.Users.Models;

namespace Application.API.Users.DTO;

public class AuthenticateOutput
{
  public User User { get; set; }
}