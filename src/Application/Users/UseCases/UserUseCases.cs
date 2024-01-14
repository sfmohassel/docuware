using Domain.Entities.Users;
using Domain.Entities.Users.Enums;
using Domain.Ports;
using Domain.Ports.Users;
using Domain.Ports.Users.Repositories;

namespace Application.Users.UseCases;

public class UserUseCases(
  IUserRepository userRepository,
  IPasswordHasher passwordHasher,
  ITransactionFactory transactionFactory)
{
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