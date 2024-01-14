using Domain.Users.Entities;

namespace Application.Users.Mappers;

public static class UserMapper
{
  public static API.Users.Models.User Map(User user)
  {
    return new API.Users.Models.User
    {
      Email = user.Email,
      Roles = user.GetRoles().Select(RoleMapper.Map).ToList(),
      UserId = user.PublicId
    };
  }
}