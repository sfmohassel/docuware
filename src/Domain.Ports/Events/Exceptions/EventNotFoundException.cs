using Common.Exceptions;

namespace Domain.Ports.Events.Exceptions;

public class EventNotFoundException() : DocuWareException(Cause.SomethingNotFound)
{
}