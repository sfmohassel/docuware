using Application.API.Users.DTO;
using Application.Users.Mappers;
using Application.Users.UseCases;
using Domain.Users.Entities;
using Domain.Users.Exceptions;
using Domain.Users.Ports;
using Domain.Users.Repositories;
using Moq;

namespace Application.Tests.Users;

public class AuthUseCasesTests
{
  private readonly AuthUseCases _authUseCases;
  private readonly Mock<IUserRepository> _userRepository;
  private readonly Mock<IPasswordHasher> _passwordHasher;

  public AuthUseCasesTests()
  {
    _userRepository = new Mock<IUserRepository>();
    _passwordHasher = new Mock<IPasswordHasher>();

    _authUseCases = new AuthUseCases(_userRepository.Object, _passwordHasher.Object);
  }

  [Fact]
  public async Task LoginShouldThrowIfUserNotFound()
  {
    var email = "sf.mohassel@outlook.com";

    _userRepository.Setup(repository => repository.GetByEmail(email))
      .Throws<UserNotFoundException>();

    await Assert.ThrowsAsync<UserNotFoundException>(() => _authUseCases.Login(new LoginInput
    {
      Email = email,
      Password = "123"
    }));
  }

  [Fact]
  public async Task LoginShouldThrowIfWrongPasswordProvided()
  {
    var email = "sf.mohassel@outlook.com";
    var user = new User(email, "pass", []);

    _userRepository.Setup(repository => repository.GetByEmail(email))
      .Returns(Task.FromResult(user));

    _passwordHasher.Setup(hasher => hasher.Verify("pass", "123")).Returns(Task.FromResult(false));

    await Assert.ThrowsAsync<UserNotFoundException>(() => _authUseCases.Login(new LoginInput
    {
      Email = email,
      Password = "123"
    }));
  }

  [Fact]
  public async Task LoginShouldReturnUserIfCredentialsAreOk()
  {
    var email = "sf.mohassel@outlook.com";
    var user = new User(email, "pass", []);

    _userRepository.Setup(repository => repository.GetByEmail(email))
      .Returns(Task.FromResult(user));

    _passwordHasher.Setup(hasher => hasher.Verify("pass", "123")).Returns(Task.FromResult(true));

    var actualUser = await _authUseCases.Login(new LoginInput
    {
      Email = email,
      Password = "123"
    });

    Assert.Equal(user.Email, actualUser.Email);
    Assert.Equal(user.GetRoles().Select(RoleMapper.Map).Order().ToList(),
      actualUser.Roles.Order().ToList());
    Assert.Equal(user.PublicId, actualUser.UserId);
  }
}