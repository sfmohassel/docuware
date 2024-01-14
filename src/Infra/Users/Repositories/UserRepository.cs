using Domain.Users.Entities;
using Domain.Users.Repositories;
using Infra.Adapters;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Users.Repositories;

public class UserRepository(EFContext efContext) : Repository<User>(efContext), IUserRepository
{
  public Task<User?> FindByEmail(string email)
  {
    return efContext.Users.Include(a => a.UserRoles)
      .FirstOrDefaultAsync(user => EF.Functions.ILike(user.Email, email));
  }

  public Task<User?> FindByPublicId(Guid publicId)
  {
    return efContext.Users.Include(a => a.UserRoles)
      .FirstOrDefaultAsync(user => user.PublicId == publicId);
  }

  public new Task<User> Save(User user)
  {
    return base.Save(user);
  }
}