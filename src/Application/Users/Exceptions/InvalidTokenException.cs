using Common.Exceptions;

namespace Application.Users.Exceptions;

public class InvalidTokenException() : DocuWareException(Cause.InvalidData)
{
}