using Common.Exceptions;

namespace Domain.Users.Exceptions;

public class UserNotFoundException() : DocuWareException(Cause.SomethingNotFound, "User was not found")
{
}