﻿using Application.API.Events.DTO;
using Application.Events.Exceptions;
using Application.Events.Mappers;
using Domain.Events.Entities;
using Domain.Events.Repositories;
using Domain.Ports;
using Domain.Users.Enums;
using Domain.Users.Services;

namespace Application.Events.UseCases;

public class EventUseCases(
  IEventRepository eventRepository,
  IRegistrationRepository registrationRepository,
  ITransactionFactory transactionFactory,
  IUserService userService)
{
  public async Task<CreateEventOutput> Create(Guid executingUserId, CreateEventInput input)
  {
    Event @event;
    using (await transactionFactory.Begin())
    {
      var executingUser = await userService.EnsureAccessTo(executingUserId, Role.EventCreator);

      var anyUnfinishedEventWithName =
        await eventRepository.AnyUnfinishedEventExistsByName(input.Name);
      if (anyUnfinishedEventWithName) throw new DuplicateEventNameException();

      @event = Event.Builder()
        .WithCreatorId(executingUser.Id)
        .WithDescription(input.Description)
        .WithEnd(input.End)
        .WithStart(input.Start)
        .WithLocation(input.Location == null ? null : AddressMapper.Map(input.Location))
        .WithName(input.Name)
        .Build();

      await eventRepository.Save(@event);
    }

    return new CreateEventOutput
    {
      Event = EventMapper.Map(@event)
    };
  }

  public async Task<ListEventsOutput> List(ListEventsInput input)
  {
    var events = (await eventRepository.FindPaginated(input))
      .Select(EventMapper.Map)
      .ToList();

    return new ListEventsOutput
    {
      Events = events
    };
  }

  public async Task<ListRegistrationsOutput> ListRegistrations(Guid eventId, ListRegistrationsInput input)
  {
    var ev = eventRepository.GetByPublicId(eventId);
    var registrations = (await registrationRepository.FindPaginated(ev.Id, input))
      .Select(RegistrationMapper.Map)
      .ToList();

    return new ListRegistrationsOutput
    {
      Registrations = registrations
    };
  }

  public async Task<RegistrationOutput> Register(Guid eventId, RegistrationInput input)
  {
    Registration registration;

    using (await transactionFactory.Begin())
    {
      var @event = eventRepository.GetByPublicId(eventId);

      if (input.Phone != null)
      {
        var exists =
          await registrationRepository.IsRegisteredInEventByPhone(@event.Id, input.Name,
            input.Phone);
        if (exists)
        {
          throw new AlreadyRegisteredByPhoneException(input.Name, input.Phone);
        }
      }

      if (input.Email != null)
      {
        var exists =
          await registrationRepository.IsRegisteredInEventByEmail(@event.Id, input.Name,
            input.Email);
        if (exists)
        {
          throw new AlreadyRegisteredByEmailException(input.Name, input.Email);
        }
      }

      registration = new Registration(@event.Id, DateTimeOffset.Now, input.Name, input.Phone,
        input.Email);
      registration = await registrationRepository.Save(registration);
    }

    return new RegistrationOutput
    {
      Registration = RegistrationMapper.Map(registration)
    };
  }
}