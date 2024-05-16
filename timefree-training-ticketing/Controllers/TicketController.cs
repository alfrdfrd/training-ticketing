using Microsoft.AspNetCore.Mvc;
using System.Net;
using timefree_training_ticketing.Adapters;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TicketController : ControllerBase
    {
        TicketAdapter ticketAdapter;
        public TicketController(TicketAdapter _ticketAdapter) 
        { 
            ticketAdapter = _ticketAdapter;


        }



        [HttpPost]
        [ProducesResponseType(typeof(ticket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTicket([FromBody] ticket ticket)
        {
            try
            {
                return Ok(await ticketAdapter.CreateTicket(ticket));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<ticket>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                return Ok(await ticketAdapter.GetTickets());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ticket>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTicket(string event_name)
        {
            try
            {
                return Ok(await ticketAdapter.GetTicket(event_name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ticket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTicket(string event_name, string location, int price, string ticket_type, string date, string new_event_name, string new_location, string new_price, string new_ticket_type, string new_date)
        {
            try
            {
                return Ok(await ticketAdapter.UpdateTicket(event_name, location, price, ticket_type, date, new_event_name, new_location, new_price, new_ticket_type, new_date));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ticket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveDeletedTicket(Guid guid)
        {
            try
            {
                return Ok(await ticketAdapter.RetrieveDeletedTicket(guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(ticket), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTicket([FromBody] ticket ticket)
        {
            try
            {
                return Ok(await ticketAdapter.DeleteTicket(ticket.guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }






    }
}
