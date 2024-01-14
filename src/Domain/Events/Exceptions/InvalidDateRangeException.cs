using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidDateRangeException()
  : DocuWareException(Cause.InvalidData, "Invalid date range")
{
}