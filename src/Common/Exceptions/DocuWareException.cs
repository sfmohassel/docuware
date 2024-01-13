namespace Common.Exceptions;

public abstract class DocuWareException : Exception
{
  protected DocuWareException(Cause cause, string message) : base(message)
  {
    Cause = cause;
  }

  protected DocuWareException(Cause cause)
  {
    Cause = cause;
  }

  public Cause Cause { get; private set; }
}