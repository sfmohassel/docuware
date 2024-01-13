using Application.API.Events.Models;

namespace Application.Events.Mappers;

public static class AddressMapper
{
  public static Address Map(Domain.Entities.Events.ValueObjects.Address address)
  {
    return new Address
    {
      Country = address.Country,
      PostalCode = address.PostalCode,
      StreetAndHouse = address.StreetAndHouse ?? "",
      City = address.City
    };
  }

  public static Domain.Entities.Events.ValueObjects.Address Map(Address address)
  {
    return new Domain.Entities.Events.ValueObjects.Address(address.Country,
      address.City, address.PostalCode, address.StreetAndHouse);
  }
}