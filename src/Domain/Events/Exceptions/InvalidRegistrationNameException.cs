using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidRegistrationNameException() : DocuWareException(Cause.InvalidData)
{
}