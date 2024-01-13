using System.Data;

namespace Domain.Ports;

public interface ITransactionFactory
{
  IDisposable Begin(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}