using Application.API.Events.Models;

namespace Application.API.Events.DTO;

public class CreateEventInput
{
  public string Name { get; set; }
  public long Start { get; set; }
  public long End { get; set; }
  public Address? Location { get; set; }
  public string? Description { get; set; }
}