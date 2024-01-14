using System.ComponentModel.DataAnnotations;

namespace Application.API.Users.DTO;

public class LoginInput
{
  [Required]
  [StringLength(maximumLength: 64)]
  public string Email { get; set; }

  [Required]
  [StringLength(maximumLength: 72)]
  public string Password { get; set; }
}