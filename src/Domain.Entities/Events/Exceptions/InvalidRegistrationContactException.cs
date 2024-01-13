using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidRegistrationContactException() : DocuWareException(Cause.InvalidData)
{
}