using Domain.Entities.Users;
using Domain.Ports.Users.Exceptions;

namespace Domain.Ports.Users.Repositories;

public interface IUserRepository
{
  User? FindByEmail(string email);

  User GetByEmail(string email)
  {
    var user = FindByEmail(email);
    return user ?? throw new UserNotFoundException();
  }

  User? FindByPublicId(Guid publicId);

  User GetByPublicId(Guid publicId)
  {
    var user = FindByPublicId(publicId);
    return user ?? throw new UserNotFoundException();
  }

  User save(User user);
}