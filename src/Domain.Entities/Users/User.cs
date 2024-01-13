using Common;
using Domain.Entities.Users.Enums;
using Domain.Entities.Users.Exceptions;

namespace Domain.Entities.Users;

public class User : Entity
{
  public User(ISet<Role> roles, string email, string password)
  {
    Roles = roles;
    Email = email;
    Password = password;

    Validate();
  }

  public ISet<Role> Roles { get; private set; }
  public string Email { get; private set; }
  public string Password { get; private set; }

  public void SetRoles(ISet<Role> roles)
  {
    Roles = roles;
  }

  public bool HasAccess(Role role)
  {
    return Roles.Contains(role);
  }

  private void Validate()
  {
    if (string.IsNullOrWhiteSpace(Email)) throw new InvalidEmailException();
  }
}