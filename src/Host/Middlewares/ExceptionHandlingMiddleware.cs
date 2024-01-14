using Common.Exceptions;

namespace Host.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (DocuWareException e)
    {
      Console.WriteLine(e);
      await Return(e.Message, e.Cause, context);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      const string msg = "Unkonwn Error! Please consult operators for more information";
      await Return(msg, Cause.Unknown, context);
    }
  }

  private static async Task Return(string msg, Cause cause, HttpContext context)
  {
    context.Response.Clear();
    context.Response.StatusCode = (int) cause;
    await context.Response.WriteAsJsonAsync(new
    {
      Message = msg
    });
  }
}