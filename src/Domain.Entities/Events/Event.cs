using Common;
using Common.CursorPagination;
using Domain.Entities.Events.Exceptions;
using Domain.Entities.Events.ValueObjects;
using EventBuilder = Domain.Entities.Events.Builders.EventBuilder;

namespace Domain.Entities.Events;

public class Event : Entity, ICursor
{
  public Event(long creatorId, string name, DateTimeOffset start, DateTimeOffset end,
    string? description, Address? location)
  {
    CreatorId = creatorId;
    Name = name;
    Start = start;
    End = end;
    Description = description;
    Location = location;

    Validate();
  }

  public static EventBuilder Builder()
  {
    return new EventBuilder();
  }

  public long CreatorId { get; set; }
  public string Name { get; private set; }
  public DateTimeOffset Start { get; private set; }
  public DateTimeOffset End { get; private set; }
  public string? Description { get; private set; }
  public Address? Location { get; private set; }

  public string Cursor => Id.ToString();

  private void Validate()
  {
    if (string.IsNullOrWhiteSpace(Name)) throw new InvalidNameException();
  }
}