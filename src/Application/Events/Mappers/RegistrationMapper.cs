using Application.API.Events.Models;

namespace Application.Events.Mappers;

public static class RegistrationMapper
{
  public static Registration Map(Domain.Entities.Events.Registration registration)
  {
    return new Registration
    {
      Email = registration.Email,
      Name = registration.Name,
      Phone = registration.Phone,
      RegistrationId = registration.PublicId
    };
  }
}