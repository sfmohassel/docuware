using Domain.Entities.Users.Enums;

namespace Application.Users.Mappers;

public static class RoleMapper
{
  public static API.Users.Models.Role Map(this Role role)
  {
    return role switch {
      Role.EventCreator => API.Users.Models.Role.EventCreator,
      _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
  }

  public static Role Map(this API.Users.Models.Role role)
  {
    return role switch {
      API.Users.Models.Role.EventCreator => Role.EventCreator,
      _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
    };
  }
}