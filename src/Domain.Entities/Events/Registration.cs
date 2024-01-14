using Common;
using Domain.Entities.Events.Exceptions;
using Domain.Entities.Users.Exceptions;

namespace Domain.Entities.Events;

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

    if (Phone != null && string.IsNullOrWhiteSpace(Phone)) throw new InvalidPhoneException();

    if (Email != null && string.IsNullOrWhiteSpace(Email)) throw new InvalidEmailException();

    if (string.IsNullOrWhiteSpace(Phone) && string.IsNullOrWhiteSpace(Email))
    {
      throw new InvalidRegistrationContactException();
    }
  }
}