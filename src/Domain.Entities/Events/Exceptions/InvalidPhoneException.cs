using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidPhoneException() : DocuWareException(Cause.InvalidData)
{
}