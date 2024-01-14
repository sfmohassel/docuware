using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidPostalCodeException() : DocuWareException(Cause.InvalidData)
{
}