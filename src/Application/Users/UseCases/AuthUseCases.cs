using Application.API.Users.DTO;
using Application.API.Users.Models;
using Application.Users.Mappers;
using Domain.Users.Exceptions;
using Domain.Users.Ports;
using Domain.Users.Repositories;

namespace Application.Users.UseCases;

public class AuthUseCases(
  IUserRepository userRepository,
  IPasswordHasher passwordHasher)
{
  public async Task<User> Login(LoginInput input)
  {
    var user = await userRepository.GetByEmail(input.Email);

    var isPasswordValid = await passwordHasher.Verify(user.Password, input.Password);
    if (!isPasswordValid) throw new UserNotFoundException();

    return UserMapper.Map(user);
  }
}