using System.Globalization;
using System.Net;
using timefree_training_ticketing.Models.Classes;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.GraphQL
{
    public partial class Mutation
    {

        public async Task<TicketResponse> CreateTicket(
            ticket input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var ticket_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var ticket_guid = Guid.NewGuid();
            var ticket_date_created = DateTime.UtcNow;

            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    input.created_by = ticket_guid;
                    input.created_by_ip = ticket_ip;
                    input.date_created = ticket_date_created;
                    await db.AddAsync(input);
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Ticket Created",
                        Ticket = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Creation!",
                        ResponseMessage = e.Message
                    };

                }

            }
        }

        public async Task<TicketResponse> UpdateTicket(
            ticket input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var ticket_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var existingTicket = await db.ticket.FindAsync(input.guid);
                    if (existingTicket == null)
                    {
                        return new TicketResponse
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "Ticket not found",
                            ResponseMessage = $"Ticket with ID {input.guid} not found"
                        };
                    }
                    existingTicket.event_name = input.event_name;
                    existingTicket.location = input.location;
                    existingTicket.price = input.price;
                    existingTicket.ticket_type = input.ticket_type;
                    existingTicket.date = input.date;
                    existingTicket.modified_by = Guid.NewGuid();
                    existingTicket.modified_by_ip = ticket_ip;
                    existingTicket.date_modified = DateTime.UtcNow;
                    existingTicket._deleted = input._deleted;
                    
                    await db.SaveChangesAsync(cancellationToken);
                    await tx.CommitAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Ticket Modified!",
                        Ticket = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Modification",
                        ResponseMessage = e.Message
                    };

                }
            }

        }

        public async Task<TicketResponse> DeleteTicket(
            ticket input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var ticket_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var existingTicket = await db.ticket.FindAsync(input.guid);
                    if (existingTicket == null)
                    {
                        return new TicketResponse
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "Ticket not found",
                            ResponseMessage = $"Ticket with ID {input.guid} not found"
                        };
                    }
                    existingTicket.modified_by = Guid.NewGuid();
                    existingTicket.modified_by_ip = ticket_ip;
                    existingTicket.date_modified = DateTime.UtcNow;
                    existingTicket._deleted = true;
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Ticket Deleted",
                        Ticket = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new TicketResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Deletion!",
                        ResponseMessage = e.Message
                    };

                }
            }

        }













    }
}
