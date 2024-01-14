using Common.CursorPagination;
using Domain.Entities.Events;
using Domain.Ports.Events.Repositories;
using Infra.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Events.Repositories;

public class EventRepository(EFContext efContext) : Repository<Event>(efContext), IEventRepository
{
  public new Task<IEnumerable<Event>> FindPaginated(PagedRequest pagedRequest)
  {
    return base.FindPaginated(pagedRequest);
  }

  public Task<Event?> FindByPublicId(Guid publicId)
  {
    return efContext.Events.FirstOrDefaultAsync(ev => ev.PublicId == publicId);
  }

  public Task<bool> AnyUnfinishedEventExistsByName(string name)
  {
    return efContext.Events.AnyAsync(ev =>
      EF.Functions.ILike(ev.Name, name));
  }

  public new Task<Event> Save(Event @event)
  {
    return base.Save(@event);
  }
}