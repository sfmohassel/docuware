using System.Data;
using Domain.Ports;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infra.Adapters;

public class TransactionFactory(EFContext efContext) : ITransactionFactory
{
  public Task<IDisposable> Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
  {
    return Task.FromResult<IDisposable>(new Trx(efContext));
  }

  private class Trx(EFContext efContext) : IDisposable, IAsyncDisposable
  {
    private readonly IDbContextTransaction transaction = efContext.Database.BeginTransaction();

    public void Dispose()
    {
      transaction.Commit();
      transaction.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
      await transaction.CommitAsync();
      await transaction.DisposeAsync();
    }
  }
}