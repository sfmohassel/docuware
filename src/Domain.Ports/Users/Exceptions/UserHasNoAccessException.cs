using Common.Exceptions;

namespace Domain.Ports.Users.Exceptions;

public class UserHasNoAccessException() : DocuWareException(Cause.NoAccess)
{
}