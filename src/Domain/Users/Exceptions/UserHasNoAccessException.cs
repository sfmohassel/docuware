using Common.Exceptions;

namespace Domain.Users.Exceptions;

public class UserHasNoAccessException() : DocuWareException(Cause.NoAccess)
{
}