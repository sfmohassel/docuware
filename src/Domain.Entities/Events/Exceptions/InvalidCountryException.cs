using Common.Exceptions;

namespace Domain.Entities.Events.Exceptions;

public class InvalidCountryException() : DocuWareException(Cause.InvalidData)
{
}