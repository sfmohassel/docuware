using Domain.Events.Exceptions;

namespace Domain.Events.ValueObjects;

public class Address
{
  public Address()
  {
  }

  public Address(string country, string city, string postalCode, string? streetAndHouse)
  {
    Country = country;
    City = city;
    PostalCode = postalCode;
    StreetAndHouse = streetAndHouse;

    Validate();
  }

  public string Country { get; private set; }
  public string City { get; private set; }
  public string PostalCode { get; private set; }
  public string? StreetAndHouse { get; private set; }

  private void Validate()
  {
    if (Country.Length != 2) throw new InvalidCountryException();

    if (string.IsNullOrWhiteSpace(City)) throw new InvalidCityException();

    if (string.IsNullOrWhiteSpace(PostalCode)) throw new InvalidPostalCodeException();
  }
}