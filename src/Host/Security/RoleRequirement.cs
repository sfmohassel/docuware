using Application.API.Users.Models;
using Microsoft.AspNetCore.Authorization;

namespace Host.Security;

public class RoleRequirement : IAuthorizationRequirement
{
  public RoleRequirement()
  {
    Role = null;
  }

  public RoleRequirement(Role role)
  {
    Role = role;
  }

  public Role? Role { get; }
}