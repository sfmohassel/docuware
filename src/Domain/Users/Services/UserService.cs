using Domain.Users.Entities;
using Domain.Users.Enums;
using Domain.Users.Exceptions;
using Domain.Users.Repositories;

namespace Domain.Users.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
  public async Task<User> EnsureAccessTo(Guid userId, Role role)
  {
    var user = await userRepository.GetByPublicId(userId);
    if (user.HasAccess(role)) return user;

    throw new UserHasNoAccessException();
  }
}