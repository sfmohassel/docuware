using Application.API.Events.Models;

namespace Application.Events.Mappers;

public static class AddressMapper
{
  public static Address Map(Domain.Events.ValueObjects.Address address)
  {
    return new Address
    {
      Country = address.Country ?? "",
      PostalCode = address.PostalCode ?? "",
      StreetAndHouse = address.StreetAndHouse ?? "",
      City = address.City ?? ""
    };
  }

  public static Domain.Events.ValueObjects.Address Map(Address address)
  {
    return address.IsEmpty()
      ? new Domain.Events.ValueObjects.Address()
      : new Domain.Events.ValueObjects.Address(address.Country,
        address.City, address.PostalCode, address.StreetAndHouse);
  }
}