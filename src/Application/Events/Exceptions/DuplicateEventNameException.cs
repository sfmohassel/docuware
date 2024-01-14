using Common.Exceptions;

namespace Application.Events.Exceptions;

public class DuplicateEventNameException() : DocuWareException(Cause.IntegrityViolation,
  "And event with this name is already registered and has not finished yet")
{
}