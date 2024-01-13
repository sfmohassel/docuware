using Application.API.Events.Models;

namespace Application.API.Events.DTO;

public class ListRegistrationsOutput
{
  public IList<Registration> Registrations { get; set; }
}