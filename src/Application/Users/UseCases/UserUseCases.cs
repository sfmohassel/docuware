using Application.API.Users.DTO;
using Application.Users.Mappers;
using Domain.Entities.Users;
using Domain.Ports;
using Domain.Ports.Users.Repositories;

namespace Application.Users.UseCases;

public class UserUseCases(
  IUserRepository userRepository,
  ITransactionFactory transactionFactory)
{
  public async Task<SetRolesOutput> SetRoles(Guid userId, SetRolesInput input)
  {
    User user;
    using (await transactionFactory.Begin())
    {
      user = await userRepository.GetByPublicId(userId);

      var roles = input.Roles.Select(RoleMapper.Map).ToHashSet();
      user.SetRoles(roles);

      user = await userRepository.Save(user);
    }

    return new SetRolesOutput
    {
      User = UserMapper.Map(user)
    };
  }
}