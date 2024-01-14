using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidNameException() : DocuWareException(Cause.InvalidData)
{
}