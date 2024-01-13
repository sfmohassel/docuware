using Common.CursorPagination;
using Domain.Entities.Events;
using Domain.Ports.Events.Repositories;
using Infra.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Events.Repositories;

public class RegistrationRepository(EFContext efContext)
  : Repository<Registration>(efContext), IRegistrationRepository
{
  public new Task<IEnumerable<Registration>> FindPaginated(PagedRequest pagedRequest)
  {
    return base.FindPaginated(pagedRequest);
  }

  public Task<bool> IsRegisteredInEventByPhone(long eventId, string name, string phone)
  {
    return efContext.Registrations
      .AnyAsync(reg => reg.EventId == eventId &&
                       reg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                       reg.Phone != null &&
                       reg.Phone.Equals(phone, StringComparison.OrdinalIgnoreCase));
  }

  public Task<bool> IsRegisteredInEventByEmail(long eventId, string name, string email)
  {
    return efContext.Registrations
      .AnyAsync(reg => reg.EventId == eventId &&
                       reg.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                       reg.Email != null &&
                       reg.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
  }

  public new Task<Registration> Save(Registration registration)
  {
    return base.Save(registration);
  }
}