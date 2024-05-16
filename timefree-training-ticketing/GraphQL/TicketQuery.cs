using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.GraphQL
{
    public partial class Query
    {

        // full list unpaged
        [UseDbContext(typeof(Ticketing))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ticket> GetTickets([ScopedService] Ticketing context)
        { 
            return context.ticket;
        }

        // paged list

        [UseDbContext(typeof(Ticketing))]
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ticket> GetTicketsPaged([ScopedService] Ticketing context)
        {
            return context.ticket;
        }

        // single record

        [UseDbContext(typeof(Ticketing))]
        [UseFirstOrDefault]
        [UseProjection]
        [UseFiltering]

        public IQueryable<ticket> GetTicket([ScopedService] Ticketing context)
        {
            return context.ticket;
        }



    }
}
