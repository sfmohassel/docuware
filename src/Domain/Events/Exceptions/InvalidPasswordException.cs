using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidPasswordException() : DocuWareException(Cause.InvalidData)
{
}