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
  public LoginOutput Login(LoginInput input)
  {
    var user = userRepository.GetByEmail(input.Email);

    var isPasswordValid = passwordHasher.Verify(user.Password, input.Password);
    if (!isPasswordValid) throw new UserNotFoundException();

    return new LoginOutput
    {
      Token = user.PublicId.ToString()
    };
  }

  public AuthenticateOutput Authenticate(AuthenticateInput input)
  {
    if (!Guid.TryParse(input.Token, out var publicId)) throw new InvalidTokenException();
    var user = userRepository.GetByPublicId(publicId);
    return new AuthenticateOutput
    {
      User = UserMapper.Map(user)
    };
  }
}