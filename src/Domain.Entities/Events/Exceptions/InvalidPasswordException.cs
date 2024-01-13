using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidPasswordException() : DocuWareException(Cause.InvalidData)
{
}