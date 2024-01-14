using Domain.Events.Entities;
using Domain.Events.Exceptions;

namespace Domain.Tests.Events;

public class EventTests
{
  [Test]
  public void NameShouldNotBeEmpty()
  {
    Assert.Throws<InvalidNameException>(() =>
      new Event(1, "", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), null, null));
  }

  [Test]
  public void StartShouldBeBeforeEnd()
  {
    Assert.Throws<InvalidDateRangeException>(() => new Event(1, "name", DateTimeOffset.Now,
      DateTimeOffset.Now.Subtract(TimeSpan.FromMilliseconds(1)), null, null));
  }

  [Test]
  public void StartShouldNotBeTheSameAsEnd()
  {
    var start = DateTimeOffset.Now;
    Assert.Throws<InvalidDateRangeException>(() => new Event(1, "name", start, start, null, null));
  }
}