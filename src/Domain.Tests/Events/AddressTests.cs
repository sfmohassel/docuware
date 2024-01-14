using Domain.Events.Exceptions;
using Domain.Events.ValueObjects;

namespace Domain.Tests.Events;

public class AddressTests
{
  [Test]
  public void CountryShouldBe2Characters()
  {
    Assert.Throws<InvalidCountryException>(() => new Address("DEE", "Munich", "88888", null));
  }

  [Test]
  public void CountryShouldNotBeEmpty()
  {
    Assert.Throws<InvalidCountryException>(() => new Address("", "Munich", "88888", null));
  }

  [Test]
  public void CityShouldNotBeEmpty()
  {
    Assert.Throws<InvalidCityException>(() => new Address("DE", "", "88888", null));
  }

  [Test]
  public void PostalCodeShouldNotBeEmpty()
  {
    Assert.Throws<InvalidPostalCodeException>(() => new Address("DE", "Munich", "", null));
  }
}