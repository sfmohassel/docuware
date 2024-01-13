using Application.API.Users.Models;
using FluentValidation;

namespace Application.API.Users.DTO;

public class SetRolesInput : AbstractValidator<SetRolesInput>
{
  public SetRolesInput()
  {
    RuleFor(a => a.Roles).NotNull();
    RuleForEach(a => a.Roles).NotNull().IsInEnum();
  }

  public IList<Role> Roles { get; set; }
}