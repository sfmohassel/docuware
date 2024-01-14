using Host.Middlewares;
using Microsoft.AspNetCore.Authorization;

namespace Host.Security;

public class RoleAccessHandler(IHttpContextAccessor httpContextAccessor)
  : AuthorizationHandler<RoleRequirement>
{
  protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
    RoleRequirement roleRequirement)
  {
    var user = httpContextAccessor.HttpContext?.GetExecutingUser();

    if (user == null)
    {
      context.Fail();
      return;
    }

    if (roleRequirement.Role == null)
    {
      context.Succeed(roleRequirement);
      return;
    }

    var hasRole = user.Roles.Contains(roleRequirement.Role.Value);

    if (!hasRole)
    {
      context.Fail();
      return;
    }

    context.Succeed(roleRequirement);
  }
}