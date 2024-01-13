using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidRegistrationNameException() : DocuWareException(Cause.InvalidData)
{
}