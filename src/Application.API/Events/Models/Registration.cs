namespace Application.API.Events.Models;

public class Registration
{
  public Guid RegistrationId { get; set; }
  public string Name { get; set; }
  public string? Phone { get; set; }
  public string? Email { get; set; }
}