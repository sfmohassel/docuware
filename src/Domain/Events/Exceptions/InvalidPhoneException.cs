using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidPhoneException() : DocuWareException(Cause.InvalidData)
{
}