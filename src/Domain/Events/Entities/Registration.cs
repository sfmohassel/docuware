using Common;
using Domain.Events.Exceptions;

namespace Domain.Events.Entities;

public class Registration : Entity
{
  public Registration()
  {
  }

  public Registration(long eventId, DateTimeOffset registeredAt, string name, string? phone,
    string? email)
  {
    EventId = eventId;
    RegisteredAt = registeredAt;
    Name = name;
    Phone = phone;
    Email = email;

    Validate();
  }

  public long EventId { get; private set; }
  public DateTimeOffset RegisteredAt { get; set; }
  public string Name { get; private set; }
  public string? Phone { get; private set; }
  public string? Email { get; private set; }

  private void Validate()
  {
    if (string.IsNullOrWhiteSpace(Name)) throw new InvalidRegistrationNameException();

    if (string.IsNullOrWhiteSpace(Phone) && string.IsNullOrWhiteSpace(Email))
    {
      throw new InvalidRegistrationContactException();
    }
  }
}