namespace Application.API.Events.Models;

public class Event
{
  public Guid EventId { get; set; }
  public string Name { get; set; }
  public long Start { get; set; }
  public long End { get; set; }
  public string Description { get; set; }
  public Address? Location { get; set; }
}