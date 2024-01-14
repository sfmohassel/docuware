using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidCityException() : DocuWareException(Cause.InvalidData, "Invalid city name")
{
}