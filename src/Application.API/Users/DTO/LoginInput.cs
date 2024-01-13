using FluentValidation;

namespace Application.API.Users.DTO;

public class LoginInput : AbstractValidator<LoginInput>
{
  public LoginInput()
  {
    RuleFor(a => a.Email).NotNull().EmailAddress();
    RuleFor(a => a.Password).NotEmpty();
  }

  public string Email { get; set; }
  public string Password { get; set; }
}