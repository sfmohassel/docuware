using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class EventNotFoundException()
  : DocuWareException(Cause.SomethingNotFound, "Event was not found")
{
}