using Domain.Entities.Users;
using Domain.Entities.Users.Enums;
using Domain.Ports.Users.Exceptions;
using Domain.Ports.Users.Repositories;

namespace Domain.Ports.Users.Services;

public class UserService(IUserRepository userRepository)
{
  public User EnsureAccessTo(Guid userId, Role role)
  {
    var user = userRepository.GetByPublicId(userId);
    if (user.HasAccess(role)) return user;

    throw new UserHasNoAccessException();
  }
}