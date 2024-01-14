using Common;

namespace Infra.Adapters.Repositories;

public abstract class Repository<T>(EFContext efContext) where T : Entity
{
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