using Domain.Events.Entities;
using Domain.Events.ValueObjects;

namespace Domain.Events.Builders;

public class EventBuilder
{
  private long _creatorId;
  private string _name;
  private DateTimeOffset _start, _end;
  private string? _description;
  private Address? _location;

  public EventBuilder WithCreatorId(long creatorId)
  {
    _creatorId = creatorId;
    return this;
  }

  public EventBuilder WithName(string name)
  {
    _name = name;
    return this;
  }

  public EventBuilder WithStart(DateTimeOffset start)
  {
    _start = start;
    return this;
  }

  public EventBuilder WithStart(long start)
  {
    return WithStart(DateTimeOffset.FromUnixTimeMilliseconds(start));
  }

  public EventBuilder WithEnd(DateTimeOffset end)
  {
    _end = end;
    return this;
  }

  public EventBuilder WithEnd(long end)
  {
    return WithEnd(DateTimeOffset.FromUnixTimeMilliseconds(end));
  }

  public EventBuilder WithDescription(string? description)
  {
    _description = description;
    return this;
  }

  public EventBuilder WithLocation(Address? location)
  {
    _location = location;
    return this;
  }

  public Event Build()
  {
    return new Event(_creatorId, _name, _start, _end, _description, _location);
  }
}