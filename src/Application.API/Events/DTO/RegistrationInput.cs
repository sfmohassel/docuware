namespace Application.API.Events.DTO;

public class RegistrationInput
{
  public Guid EventId { get; set; }
  public string Name { get; set; }
  public string? Phone { get; set; }
  public string? Email { get; set; }
}