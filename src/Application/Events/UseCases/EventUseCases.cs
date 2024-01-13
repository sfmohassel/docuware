using Application.API.Events.DTO;
using Application.Events.Exceptions;
using Application.Events.Mappers;
using Domain.Entities.Events;
using Domain.Entities.Users.Enums;
using Domain.Ports;
using Domain.Ports.Events.Repositories;
using Domain.Ports.Users.Services;

namespace Application.Events.UseCases;

public class EventUseCases(
  IEventRepository eventRepository,
  IRegistrationRepository registrationRepository,
  ITransactionFactory transactionFactory,
  UserService userService)
{
  public CreateEventOutput Create(Guid executingUserId, CreateEventInput input)
  {
    Event @event;
    using (transactionFactory.Begin())
    {
      var executingUser = userService.EnsureAccessTo(executingUserId, Role.EventCreator);

      var anyUnfinishedEventWithName = eventRepository.AnyUnfinishedEventExistsByName(input.Name);
      if (anyUnfinishedEventWithName) throw new DuplicateEventNameException();

      @event = Event.Builder()
        .WithCreatorId(executingUser.Id)
        .WithDescription(input.Description)
        .WithEnd(input.End)
        .WithStart(input.Start)
        .WithLocation(input.Location == null ? null : AddressMapper.Map(input.Location))
        .WithName(input.Name)
        .Build();

      eventRepository.save(@event);
    }

    return new CreateEventOutput
    {
      Event = EventMapper.Map(@event)
    };
  }

  public ListEventsOutput List(ListEventsInput input)
  {
    var events = eventRepository.FindPaginated(input)
      .Select(EventMapper.Map)
      .ToList();

    return new ListEventsOutput
    {
      Events = events
    };
  }

  public ListRegistrationsOutput ListRegistrations(ListRegistrationsInput input)
  {
    var registrations = registrationRepository.FindPaginated(input)
      .Select(RegistrationMapper.Map)
      .ToList();

    return new ListRegistrationsOutput
    {
      Registrations = registrations
    };
  }

  public RegistrationOutput Register(RegistrationInput input)
  {
    Registration registration;

    using (transactionFactory.Begin())
    {
      if (input.Phone != null)
      {
        var exists =
          registrationRepository.IsRegisteredInEventByPhone(input.EventId, input.Name, input.Phone);
        if (exists)
        {
          throw new AlreadyRegisteredByPhoneException(input.Name, input.Phone);
        }
      }

      if (input.Email != null)
      {
        var exists =
          registrationRepository.IsRegisteredInEventByEmail(input.EventId, input.Name, input.Email);
        if (exists)
        {
          throw new AlreadyRegisteredByEmailException(input.Name, input.Email);
        }
      }

      var @event = eventRepository.GetByPublicId(input.EventId);

      registration = new Registration(@event.Id, DateTimeOffset.Now, input.Name, input.Phone,
        input.Email);
      registration = registrationRepository.save(registration);
    }

    return new RegistrationOutput
    {
      Registration = RegistrationMapper.Map(registration)
    };
  }
}