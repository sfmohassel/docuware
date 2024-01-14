using Common.Exceptions;

namespace Domain.Events.Exceptions;

public class InvalidRegistrationContactException()
  : DocuWareException(Cause.InvalidData, "Either provide Email or Phone as contact information")
{
}