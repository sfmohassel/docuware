using Application.API.Events.Models;

namespace Application.API.Events.DTO;

public class ListEventsOutput
{
  public IList<Event> Events { get; set; }
}