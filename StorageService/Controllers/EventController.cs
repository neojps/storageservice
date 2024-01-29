namespace Presentation.Api.Controllers
{
    using Application.Dto;
    using Application.Service.Event;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1.0")]
    [Route("/event")]
    [ApiController]
    [Produces("application/json")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None)]
    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostEventAsync(AddEventRequest addEventRequest)
        {
            if (addEventRequest is null || string.IsNullOrEmpty(addEventRequest.EventContent))
            {
                return this.BadRequest("Invalid data to storage");
            }

            await this.eventService.StorageEventAsync(addEventRequest);

            return this.Ok();
        }
    }
}
