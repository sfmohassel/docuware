namespace Application.API.Events.Models;

public class Address
{
  public string? Country { get; set; }
  public string? City { get; set; }
  public string? PostalCode { get; set; }
  public string? StreetAndHouse { get; set; }

  public bool IsEmpty()
  {
    return string.IsNullOrWhiteSpace(Country) &&
           string.IsNullOrWhiteSpace(City) &&
           string.IsNullOrWhiteSpace(PostalCode) &&
           string.IsNullOrWhiteSpace(StreetAndHouse);
  }
}