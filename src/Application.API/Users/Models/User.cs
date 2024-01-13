namespace Application.API.Users.Models;

public class User
{
  public Guid UserId { get; set; }
  public IList<Role> Roles { get; set; }
  public string Email { get; set; }
}