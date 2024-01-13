using Common.Exceptions;

namespace Domain.Entities.Users.Exceptions;

public class InvalidEmailException() : DocuWareException(Cause.InvalidData)
{
}