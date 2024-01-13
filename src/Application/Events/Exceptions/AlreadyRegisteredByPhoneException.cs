using Common.Exceptions;

namespace Application.Events.Exceptions;

public class AlreadyRegisteredByPhoneException(string name, string phone)
  : DocuWareException(Cause.IntegrityViolation,
    $"A person with name '{name}' and phone '{phone}' is already registered in event");