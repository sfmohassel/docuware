using Application.Users.Mappers;
using Domain.Ports;
using Domain.Users.Entities;
using Domain.Users.Enums;
using Domain.Users.Ports;
using Domain.Users.Repositories;

namespace Application.Users.UseCases;

public class UserUseCases(
  IUserRepository userRepository,
  IPasswordHasher passwordHasher,
  ITransactionFactory transactionFactory)
{
  public async Task<API.Users.Models.User> GetById(Guid userId)
  {
    var user = await userRepository.GetByPublicId(userId);
    return UserMapper.Map(user);
  }

  public async Task SeedAdmin(string email, string password)
  {
    using (await transactionFactory.Begin())
    {
      var admin = await userRepository.FindByEmail(email);
      if (admin == null)
      {
        admin = new User(email, await passwordHasher.Hash(password), new[] { Role.EventCreator });
        await userRepository.Save(admin);
      }
    }
  }
}