using Common;
using Domain.Entities.Users.Enums;
using Domain.Entities.Users.Exceptions;

namespace Domain.Entities.Users;

public class User : Entity
{
  public User(string email, string password)
  {
    Email = email;
    Password = password;

    Validate();
  }

  public IList<UserRole> UserRoles { get; private set; }
  public string Email { get; private set; }
  public string Password { get; private set; }

  public void SetRoles(IEnumerable<Role> roles)
  {
    UserRoles = roles.Select(role => new UserRole
    {
      Role = role,
      User = this,
      UserId = Id
    }).ToList();
  }

  public ISet<Role> GetRoles()
  {
    return UserRoles.Select(userRole => userRole.Role).ToHashSet();
  }

  public bool HasAccess(Role role)
  {
    return UserRoles.Any(userRole => userRole.Role == role);
  }

  private void Validate()
  {
    if (string.IsNullOrWhiteSpace(Email)) throw new InvalidEmailException();
  }
}