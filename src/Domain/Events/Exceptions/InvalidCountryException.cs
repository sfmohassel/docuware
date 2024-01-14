using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidCountryException() : DocuWareException(Cause.InvalidData)
{
}