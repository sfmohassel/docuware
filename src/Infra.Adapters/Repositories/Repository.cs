using Common;
using Common.CursorPagination;
using Microsoft.EntityFrameworkCore;

namespace Infra.Adapters.Repositories;

public abstract class Repository<T>(EFContext efContext) where T : Entity
{
  protected async Task<IEnumerable<T>> FindPaginated(PagedRequest pagedRequest)
  {
    var backward = pagedRequest.Backward ?? false;
    IQueryable<T> q = efContext.Set<T>();
    q = backward ? q.OrderByDescending(ev => ev.Id) : q.OrderBy(ev => ev.Id);
    if (long.TryParse(pagedRequest.Cursor, out var cursor))
    {
      q = backward ? q.Where(ev => ev.Id < cursor) : q.Where(ev => ev.Id > cursor);
    }

    return await q.Take(pagedRequest.PageSize).ToListAsync();
  }

  protected async Task<T> Save(T item)
  {
    if (item.IsNew())
    {
      efContext.Set<T>().Add(item);
    }

    await efContext.SaveChangesAsync();
    return item;
  }
}