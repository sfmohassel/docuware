using Application.API.Events.DTO;
using Application.Events.UseCases;
using Host.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("events")]
public class EventController(EventUseCases eventUseCases, IHttpContextAccessor httpContextAccessor)
{
  [Authorize("EventCreator")]
  [HttpPost]
  public Task<CreateEventOutput> Create([FromBody] CreateEventInput input)
  {
    var executingUser = httpContextAccessor.HttpContext!.GetExecutingUser();
    return eventUseCases.Create(executingUser!.UserId, input);
  }

  [HttpGet]
  public Task<ListEventsOutput> ListEvents([FromQuery] ListEventsInput input)
  {
    return eventUseCases.List(input);
  }
}