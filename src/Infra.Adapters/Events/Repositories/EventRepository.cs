using Common.CursorPagination;
using Domain.Entities.Events;
using Domain.Ports.Events.Repositories;
using Infra.Adapters.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Events.Repositories;

public class EventRepository(EFContext efContext) : Repository<Event>(efContext), IEventRepository
{
  public new async Task<IEnumerable<Event>> FindPaginated(PagedRequest pagedRequest)
  {
    var backward = pagedRequest.Backward ?? false;
    IQueryable<Event> q = efContext.Events;
    q = backward ? q.OrderByDescending(ev => ev.Start) : q.OrderBy(ev => ev.Start);
    if (long.TryParse(pagedRequest.Cursor, out var cursorLong))
    {
      var cursor = DateTimeOffset.FromUnixTimeMilliseconds(cursorLong);
      q = backward ? q.Where(ev => ev.Start < cursor) : q.Where(ev => ev.Start > cursor);
    }

    return await q.Take(pagedRequest.PageSize).ToListAsync();
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