using Application.API.Users.Models;

namespace Host.Middlewares;

public static class HttpContextExtensions
{
  private const string KEY = "EXECUTING_USER";

  public static User? GetExecutingUser(this HttpContext context)
  {
    return context.Items[KEY] as User;
  }

  public static void SetExecutingUser(this HttpContext context, User user)
  {
    context.Items[KEY] = user;
  }
}