using Common;
using Domain.Users.Enums;
using Domain.Users.Exceptions;

namespace Domain.Users.Entities;

public class User : Entity
{
  public User()
  {
  }

  public User(string email, string password, IEnumerable<Role> roles)
  {
    Email = email;
    Password = password;
    UserRoles = roles.Select(role => new UserRole
    {
      Role = role,
      UserId = Id
    }).ToList();

    Validate();
  }

  public ICollection<UserRole> UserRoles { get; }
  public string Email { get; private set; }
  public string Password { get; private set; }

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

  public class UserRole
  {
    public long UserId { get; init; }
    public Role Role { get; set; }
  }
}