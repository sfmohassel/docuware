using Application.API.Events.DTO;
using Application.Events.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("events/{eventId:guid}/registrations")]
public class RegistrationController(EventUseCases eventUseCases)
{
  [HttpPost]
  public Task<RegistrationOutput> Register([FromRoute] Guid eventId,
    [FromBody] RegistrationInput input)
  {
    return eventUseCases.Register(eventId, input);
  }

  [Authorize("EventCreator")]
  [HttpGet]
  public Task<ListRegistrationsOutput> ListRegistrations([FromRoute] Guid eventId,
    [FromQuery] ListRegistrationsInput input)
  {
    return eventUseCases.ListRegistrations(eventId, input);
  }
}