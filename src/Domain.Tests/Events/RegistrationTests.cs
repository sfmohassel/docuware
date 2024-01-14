using Domain.Events.Entities;
using Domain.Events.Exceptions;

namespace Domain.Tests.Events;

public class RegistrationTests
{
  [Test]
  public void NameShouldNotBeEmpty()
  {
    Assert.Throws<InvalidRegistrationNameException>(() =>
      new Registration(1, DateTimeOffset.Now, "", "phone", "email"));
  }

  [Test]
  public void EitherPhoneOrEmailShouldNotBeEmpty()
  {
    Assert.Throws<InvalidRegistrationContactException>(() =>
      new Registration(1, DateTimeOffset.Now, "saeed", null, null));

    var reg = new Registration(1, DateTimeOffset.Now, "saeed", "phone", null);
    reg = new Registration(1, DateTimeOffset.Now, "saeed", null, "email");
  }
}