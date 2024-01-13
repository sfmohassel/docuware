using Common.Exceptions;

namespace Application.Events.Exceptions;

public class DuplicateEventNameException() : DocuWareException(Cause.IntegrityViolation)
{
}