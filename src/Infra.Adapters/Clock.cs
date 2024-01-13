using Domain.Ports;

namespace Infra.Adapters;

public class Clock : IClock
{
  public DateTime Now()
  {
    return DateTime.Now;
  }
}