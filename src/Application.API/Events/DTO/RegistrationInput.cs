namespace Application.API.Events.DTO;

public class RegistrationInput
{
  public string Name { get; set; }
  public string? Phone { get; set; }
  public string? Email { get; set; }
}