using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidNameException() : DocuWareException(Cause.InvalidData)
{
}