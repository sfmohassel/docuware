using Application.API.Events.Models;

namespace Application.Events.Mappers;

public static class EventMapper
{
  public static Event Map(Domain.Events.Entities.Event @event)
  {
    return new Event
    {
      EventId = @event.PublicId,
      Description = @event.Description ?? "",
      End = @event.End.ToUnixTimeMilliseconds(),
      Location = AddressMapper.Map(@event.Location),
      Name = @event.Name,
      Start = @event.Start.ToUnixTimeMilliseconds()
    };
  }
}