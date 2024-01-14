using Domain.Users.Entities;
using Domain.Users.Enums;

namespace Domain.Users.Services;

public interface IUserService
{
  Task<User> EnsureAccessTo(Guid userId, Role role);
}