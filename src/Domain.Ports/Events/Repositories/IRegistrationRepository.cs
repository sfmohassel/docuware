using Common.CursorPagination;
using Domain.Entities.Events;

namespace Domain.Ports.Events.Repositories;

public interface IRegistrationRepository
{
  IList<Registration> FindPaginated(PagedRequest pagedRequest);
  bool IsRegisteredInEventByPhone(Guid eventId, string name, string phone);
  bool IsRegisteredInEventByEmail(Guid eventId, string name, string email);
  Registration save(Registration registration);
}