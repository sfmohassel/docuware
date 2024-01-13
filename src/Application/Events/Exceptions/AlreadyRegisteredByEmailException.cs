using Common.Exceptions;

namespace Application.Events.Exceptions;

public class AlreadyRegisteredByEmailException(string name, string email)
  : DocuWareException(Cause.IntegrityViolation,
    $"A person with name '{name}' and email '{email}' is already registered in event");