using Common.Exceptions;

namespace Domain.Users.Exceptions;

public class InvalidEmailException() : DocuWareException(Cause.InvalidData)
{
}