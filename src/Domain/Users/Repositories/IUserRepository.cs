using Domain.Users.Entities;
using Domain.Users.Exceptions;

namespace Domain.Users.Repositories;

public interface IUserRepository
{
  Task<User?> FindByEmail(string email);

  async Task<User> GetByEmail(string email)
  {
    var user = await FindByEmail(email);
    return user ?? throw new UserNotFoundException();
  }

  Task<User?> FindByPublicId(Guid publicId);

  async Task<User> GetByPublicId(Guid publicId)
  {
    var user = await FindByPublicId(publicId);
    return user ?? throw new UserNotFoundException();
  }

  Task<User> Save(User user);
}