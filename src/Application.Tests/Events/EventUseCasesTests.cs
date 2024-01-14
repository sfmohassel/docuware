using System.Data;
using Application.API.Events.DTO;
using Application.API.Events.Models;
using Application.Events.Exceptions;
using Application.Events.UseCases;
using Domain.Events.Repositories;
using Domain.Ports;
using Domain.Users.Entities;
using Domain.Users.Enums;
using Domain.Users.Exceptions;
using Domain.Users.Services;
using Moq;

namespace Application.Tests.Events;

public class EventUseCasesTests
{
  private readonly Mock<IEventRepository> _eventRepository;
  private readonly Mock<IRegistrationRepository> _registrationRepository;
  private readonly Mock<ITransactionFactory> _transactionFactory;
  private readonly Mock<IUserService> _userService;
  private readonly EventUseCases _eventUseCases;

  public EventUseCasesTests()
  {
    _eventRepository = new Mock<IEventRepository>();
    _registrationRepository = new Mock<IRegistrationRepository>();
    _transactionFactory = new Mock<ITransactionFactory>();
    _userService = new Mock<IUserService>();
    _eventUseCases = new EventUseCases(_eventRepository.Object,
      _registrationRepository.Object, _transactionFactory.Object,
      _userService.Object);

    _transactionFactory.Setup(tr => tr.Begin(IsolationLevel.ReadCommitted))
      .Returns(Task.FromResult<IDisposable>(new Dummy()));
  }

  [Fact]
  public async Task CreateShouldThrowIfExecutingUserHasNoAccess()
  {
    var executingUserId = Guid.NewGuid();

    _userService.Setup(service => service.EnsureAccessTo(executingUserId, Role.EventCreator))
      .Throws<UserHasNoAccessException>();

    await Assert.ThrowsAsync<UserHasNoAccessException>(() => _eventUseCases.Create(executingUserId,
      new CreateEventInput
      {
        Description = null,
        End = DateTimeOffset.Now.AddDays(1).ToUnixTimeMilliseconds(),
        Start = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        Location = null,
        Name = "eve"
      }));
  }

  [Fact]
  public async Task CreateShouldThrowIfAnyUnfinishedEventWithTheSameNameExists()
  {
    var user = new User("sf.mohassel@outlook.com", "123", []);
    var eventName = "good event";

    _userService.Setup(service => service.EnsureAccessTo(user.PublicId, Role.EventCreator))
      .Returns(Task.FromResult(user));

    _eventRepository.Setup(repository => repository.AnyUnfinishedEventExistsByName(eventName))
      .Returns(Task.FromResult(true));

    await Assert.ThrowsAsync<DuplicateEventNameException>(() => _eventUseCases.Create(user.PublicId,
      new CreateEventInput
      {
        Description = null,
        End = DateTimeOffset.Now.AddDays(1).ToUnixTimeMilliseconds(),
        Start = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
        Location = null,
        Name = eventName
      }));
  }

  [Fact]
  public async Task CreateShouldReturnEvent()
  {
    var user = new User("sf.mohassel@outlook.com", "123", []);
    const string eventName = "good event";

    _userService.Setup(service => service.EnsureAccessTo(user.PublicId, Role.EventCreator))
      .Returns(Task.FromResult(user));

    _eventRepository.Setup(repository => repository.AnyUnfinishedEventExistsByName(eventName))
      .Returns(Task.FromResult(false));

    var input = new CreateEventInput
    {
      Description = null,
      End = DateTimeOffset.Now.AddDays(1).ToUnixTimeMilliseconds(),
      Start = DateTimeOffset.Now.ToUnixTimeMilliseconds(),
      Location = new Address
      {
        Country = "DE",
        City = "Munich",
        PostalCode = "88888"
      },
      Name = eventName
    };
    var output = await _eventUseCases.Create(user.PublicId, input);
    var actualEvent = output.Event;
    Assert.NotNull(actualEvent);

    Assert.Equal("", actualEvent.Description);
    Assert.Equal(input.End, actualEvent.End);
    Assert.Equal(input.Start, actualEvent.Start);
    Assert.NotNull(actualEvent.Location);
    Assert.Equal(input.Location.Country, actualEvent.Location.Country);
    Assert.Equal(input.Location.City, actualEvent.Location.City);
    Assert.Equal(input.Location.PostalCode, actualEvent.Location.PostalCode);
    Assert.Equal("", actualEvent.Location.StreetAndHouse);
    Assert.Equal(input.Name, actualEvent.Name);
  }

  class Dummy : IDisposable
  {
    public void Dispose()
    {
    }
  }
}