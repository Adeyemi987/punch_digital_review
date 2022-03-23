using CalendarService.Core.Interfaces.Services.CoreServices;
using CalendarService.Core.Models.APIResponse;
using CalendarService.Core.Models.Constants;
using CalendarService.Core.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarService.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]

    public class CalendarController : ControllerBase 
    {
        private readonly IEventManagementService _eventManagementService;
        public CalendarController(IEventManagementService eventManagementService)
        {
            _eventManagementService = eventManagementService;
        }

        /// <summary>
        /// Add a new Event.
        /// </summary>
        ///<response code="201">Returned for event added successfuly</response>
        ///
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddEventAsync([FromBody] EventDTO payload) 
        {
            payload.Id = await _eventManagementService.AddNewEventAsync(payload);
            return CreatedAtAction(nameof(GetAllEventsByQueryStrings), new { id = payload.Id }, payload);
        }

        /// <summary>
        /// Delete an existing event.
        /// </summary>
        ///<response code="204">Returned when event deleted successfully</response>
        ///<response code="404">Returned when event not found</response>
        ///
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEventAsync(long id) 
        {
            var isDeleted = await _eventManagementService.DeleteEventAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Edit an existing event.
        /// </summary>
        ///<response code="204">Returned when event edited successfully</response>
        ///<response code="400">Returned when id in request route doesnt match request body</response>
        ///<response code="404">Returned when event not found</response>
        ///
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditEventAsync(long id, [FromBody] EventDTO payload) 
        {
            if (id != payload.Id)
            {
                return BadRequest();
            }
            var isEdited = await _eventManagementService.EditEventAsync(payload);
            if (!isEdited)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Get all events.
        /// </summary>
        ///<response code="200">Returned all events or empty array</response>
        ///
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEventsAsync()
        {
            var allEvents = await _eventManagementService.GetAllEventsAsync();
            return Ok(allEvents);
        }

        /// <summary>
        /// Get events by query strings Options: eventOrganizer, id, location, name .
        /// </summary>
        ///<response code="200">Returned all events by organizer, id, location, name as specified in query string or empty array</response>
        ///
        [HttpGet("query")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEventsByQueryStrings([FromQuery] string eventOrganizer, [FromQuery] long id, [FromQuery] string location, [FromQuery] string name) 
        {
            if (!string.IsNullOrEmpty(eventOrganizer))
            {
                var allEvents = await _eventManagementService.GetAllEventsByOrganizerAsync(eventOrganizer);
                return Ok(allEvents);
            }
            else if(id != 0)
            {
                var evnt = await _eventManagementService.GetEventByIdAsync(id);
                return Ok(evnt);
            }
            else if(!string.IsNullOrEmpty(location))
            {
                var allEvents = await _eventManagementService.GetAllEventsByLocationAsync(location);
                return Ok(allEvents); 
            }
            else
            {
                var evnt = await _eventManagementService.GetEventByNameAsync(name);
                return Ok(evnt);
            }       
        }

        /// <summary>
        /// Get all events sorted by time in descending order.
        /// </summary>
        ///<response code="200">Returned all events by organizer, id, location, name as specified in query string or empty array</response>
        ///
        [HttpGet("sort")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSortedEventsAsync() 
        {
            var allEvents = await _eventManagementService.GetAllEventsSortedByTimeAsync();
            return Ok(allEvents);
        }

    }

}
