using Domain.Entities.Users.Enums;

namespace Domain.Entities.Users;

public class UserRole
{
  public User User { get; set; }
  public long UserId { get; set; }
  public Role Role { get; set; }
}