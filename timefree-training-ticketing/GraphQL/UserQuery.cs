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
        public IQueryable<user> GetUsers([ScopedService] Ticketing context)
        {
            return context.user;
        }

        // paged list

        [UseDbContext(typeof(Ticketing))]
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<user> GetUsersPaged([ScopedService] Ticketing context)
        {
            return context.user;
        }

        // single record

        [UseDbContext(typeof(Ticketing))]
        [UseFirstOrDefault]
        [UseProjection]
        [UseFiltering]

        public IQueryable<user> GetUser([ScopedService] Ticketing context)
        {
            return context.user;
        }


    }
}
