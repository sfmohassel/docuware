using Common.Exceptions;

namespace Domain.Ports.Users.Exceptions;

public class UserNotFoundException() : DocuWareException(Cause.SomethingNotFound)
{
}