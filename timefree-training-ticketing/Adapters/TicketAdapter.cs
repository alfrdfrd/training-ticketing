using Microsoft.EntityFrameworkCore;
using System.Globalization;
using timefree_training_ticketing.Models.EF.Ticketing;


namespace timefree_training_ticketing.Adapters
{
    public class TicketAdapter
    {
        Ticketing db;
        IHttpContextAccessor accessor;

        public TicketAdapter(Ticketing _ticketing, IHttpContextAccessor _accessor)
        {
            db = _ticketing;
            accessor = _accessor;
        }


        // Create

        public async Task<ticket> CreateTicket(ticket ticket)
        {
            var ticket_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            ticket.created_by_ip = ticket_ip;
            await db.ticket.AddAsync(ticket);
            await db.SaveChangesAsync();
            return ticket;
        }


        // Read

        public async Task<List<ticket>> GetTickets()
        {
            return await db.ticket.ToListAsync();
        }

        public async Task<List<ticket>> GetTicket(string event_name)
        {
            return await db.ticket.Where(x =>  x.event_name.Contains(event_name) && !x._deleted).ToListAsync();
        }


        // Update

        public async Task<List<ticket>> UpdateTicket(string event_name, string location, int price, string ticket_type, string date, string new_event_name, string new_location, string new_price, string new_ticket_type, string new_date)
        { 

            var dateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffffff";
            DateTime ticket_date = DateTime.ParseExact(date, dateTimeFormat , CultureInfo.InvariantCulture);
            var ticketToUpdate = await db.ticket
                .Where(x => x.event_name.Contains(event_name) && x.location.Contains(location) && x.price.Equals(price) && x.ticket_type.Contains(ticket_type) && x.date.Equals(ticket_date) && !x._deleted)
                .ToListAsync();

            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            DateTime newDate = DateTime.ParseExact(new_date, dateTimeFormat, CultureInfo.InvariantCulture);

            foreach (var ticket in ticketToUpdate)
            {
                ticket.event_name = new_event_name;
                ticket.location = new_location;
                ticket.price = int.Parse(new_price);
                ticket.date = newDate;
                ticket.ticket_type = new_ticket_type;
                ticket.date_modified = DateTime.UtcNow;
                ticket.modified_by = Guid.NewGuid();
                ticket.modified_by_ip = modified_by_ip;
            }
            await db.SaveChangesAsync();
            return ticketToUpdate;

        }

        public async Task<List<ticket>> RetrieveDeletedTicket(Guid guid)
        {
            var ticketToRetrieve = await db.ticket.Where(x => x.guid.Equals(guid) && x._deleted).ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var ticket in ticketToRetrieve)
            {
                ticket._deleted = false;
                ticket.date_modified = DateTime.UtcNow;
                ticket.modified_by = Guid.NewGuid();
                ticket.modified_by_ip = modified_by_ip;
            }


            await db.SaveChangesAsync();

            return ticketToRetrieve;

        }

            // Delete


            public async Task<List<ticket>> DeleteTicket(Guid guid)
        {
            var ticketToDelete = await db.ticket
                .Where(x => x.guid.Equals(guid) && !x._deleted)
                .ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach(var ticket in ticketToDelete)
            {
                ticket._deleted = true;
                ticket.date_modified = DateTime.UtcNow;
                ticket.modified_by = Guid.NewGuid();
                ticket.modified_by_ip = modified_by_ip;
            }


            await db.SaveChangesAsync();

            return ticketToDelete;
        }







    }
}
