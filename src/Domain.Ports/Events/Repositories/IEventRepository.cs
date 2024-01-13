using Common.CursorPagination;
using Domain.Entities.Events;
using Domain.Ports.Events.Exceptions;

namespace Domain.Ports.Events.Repositories;

public interface IEventRepository
{
  IEnumerable<Event> FindPaginated(PagedRequest pagedRequest);
  Event? FindByPublicId(Guid publicId);

  Event GetByPublicId(Guid publicId)
  {
    return FindByPublicId(publicId) ?? throw new EventNotFoundException();
  }

  bool AnyUnfinishedEventExistsByName(string name);

  Event save(Event @event);
}