using Common.CursorPagination;
using Domain.Entities.Events;
using Domain.Ports.Events.Exceptions;

namespace Domain.Ports.Events.Repositories;

public interface IEventRepository
{
  Task<IEnumerable<Event>> FindPaginated(PagedRequest pagedRequest);
  Task<Event?> FindByPublicId(Guid publicId);

  async Task<Event> GetByPublicId(Guid publicId)
  {
    var @event = await FindByPublicId(publicId);
    return @event ?? throw new EventNotFoundException();
  }

  Task<bool> AnyUnfinishedEventExistsByName(string name);

  Task<Event> Save(Event @event);
}