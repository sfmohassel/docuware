using Common.CursorPagination;
using Domain.Events.Entities;

namespace Domain.Events.Repositories;

public interface IRegistrationRepository
{
  Task<IEnumerable<Registration>> FindPaginated(long eventId, PagedRequest pagedRequest);
  Task<bool> IsRegisteredInEventByPhone(long eventId, string name, string phone);
  Task<bool> IsRegisteredInEventByEmail(long eventId, string name, string email);
  Task<Registration> Save(Registration registration);
}