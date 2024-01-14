using Domain.Entities.Users;
using Domain.Ports.Users.Repositories;
using Infra.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Users.Repositories;

public class UserRepository(EFContext efContext) : Repository<User>(efContext), IUserRepository
{
  public Task<User?> FindByEmail(string email)
  {
    return efContext.Users.FirstOrDefaultAsync(user => EF.Functions.ILike(user.Email, email));
  }

  public Task<User?> FindByPublicId(Guid publicId)
  {
    return efContext.Users.FirstOrDefaultAsync(user => user.PublicId == publicId);
  }

  public new Task<User> Save(User user)
  {
    return base.Save(user);
  }
}