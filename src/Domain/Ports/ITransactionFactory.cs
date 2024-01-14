using System.Data;

namespace Domain.Ports;

public interface ITransactionFactory
{
  Task<IDisposable> Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}