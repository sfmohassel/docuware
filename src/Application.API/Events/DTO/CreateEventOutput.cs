using Application.API.Events.Models;

namespace Application.API.Events.DTO;

public class CreateEventOutput
{
  public Event Event { get; set; }
}