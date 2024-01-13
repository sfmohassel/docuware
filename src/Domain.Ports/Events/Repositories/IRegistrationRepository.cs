using Common.CursorPagination;
using Domain.Entities.Events;

namespace Domain.Ports.Events.Repositories;

public interface IRegistrationRepository
{
  Task<IEnumerable<Registration>> FindPaginated(PagedRequest pagedRequest);
  Task<bool> IsRegisteredInEventByPhone(long eventId, string name, string phone);
  Task<bool> IsRegisteredInEventByEmail(long eventId, string name, string email);
  Task<Registration> Save(Registration registration);
}