using Domain.Events.Exceptions;
using Domain.Users.Entities;
using Domain.Users.Exceptions;

namespace Domain.Tests.Users;

public class UserTests
{
  [Test]
  public void EmailShouldNotBeEmpty()
  {
    Assert.Throws<InvalidEmailException>(() => new User("", "pass", []));
  }

  [Test]
  public void PasswordShouldNotBeEmpty()
  {
    Assert.Throws<InvalidPasswordException>(() => new User("email@example.com", "", []));
  }
}