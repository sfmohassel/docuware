using Application.API.Users.DTO;
using Application.Users.Exceptions;
using Application.Users.Mappers;
using Domain.Ports.Users;
using Domain.Ports.Users.Exceptions;
using Domain.Ports.Users.Repositories;

namespace Application.Users.UseCases;

public class AuthUseCases(
  IUserRepository userRepository,
  IPasswordHasher passwordHasher)
{
  public async Task<LoginOutput> Login(LoginInput input)
  {
    var user = await userRepository.GetByEmail(input.Email);

    var isPasswordValid = await passwordHasher.Verify(user.Password, input.Password);
    if (!isPasswordValid) throw new UserNotFoundException();

    return new LoginOutput
    {
      Token = user.PublicId.ToString(),
      Roles = user.GetRoles().Select(RoleMapper.Map).ToList()
    };
  }

  public async Task<AuthenticateOutput> Authenticate(AuthenticateInput input)
  {
    if (!Guid.TryParse(input.Token, out var publicId)) throw new InvalidTokenException();
    var user = await userRepository.GetByPublicId(publicId);
    return new AuthenticateOutput
    {
      User = UserMapper.Map(user)
    };
  }
}