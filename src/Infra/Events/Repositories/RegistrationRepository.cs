using Common.CursorPagination;
using Domain.Events.Entities;
using Domain.Events.Repositories;
using Infra.Adapters;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Events.Repositories;

public class RegistrationRepository(EFContext efContext)
  : Repository<Registration>(efContext), IRegistrationRepository
{
  public new async Task<IEnumerable<Registration>> FindPaginated(long eventId,
    PagedRequest pagedRequest)
  {
    var backward = pagedRequest.Backward ?? false;
    IQueryable<Registration> q = efContext.Registrations.Where(reg => reg.EventId == eventId);
    q = backward ? q.OrderByDescending(ev => ev.RegisteredAt) : q.OrderBy(ev => ev.RegisteredAt);
    if (long.TryParse(pagedRequest.Cursor, out var cursorLong))
    {
      var cursor = DateTimeOffset.FromUnixTimeMilliseconds(cursorLong);
      q = backward
        ? q.Where(ev => ev.RegisteredAt < cursor)
        : q.Where(ev => ev.RegisteredAt > cursor);
    }

    return await q.Take(pagedRequest.PageSize).ToListAsync();
  }

  public Task<bool> IsRegisteredInEventByPhone(long eventId, string name, string phone)
  {
    return efContext.Registrations
      .AnyAsync(reg => reg.EventId == eventId &&
                       EF.Functions.ILike(reg.Name, name) &&
                       reg.Phone != null &&
                       EF.Functions.ILike(reg.Phone, phone));
  }

  public Task<bool> IsRegisteredInEventByEmail(long eventId, string name, string email)
  {
    return efContext.Registrations
      .AnyAsync(reg => reg.EventId == eventId &&
                       EF.Functions.ILike(reg.Name, name) &&
                       reg.Email != null &&
                       EF.Functions.ILike(reg.Email, email));
  }

  public new Task<Registration> Save(Registration registration)
  {
    return base.Save(registration);
  }
}