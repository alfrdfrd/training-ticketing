
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
        public IQueryable<order> GetOrders([ScopedService] Ticketing context)
        {
            return context.order;
        }

        // paged list

        [UseDbContext(typeof(Ticketing))]
        [UseOffsetPaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<order> GetOrdersPaged([ScopedService] Ticketing context)
        {
            return context.order;
        }

        // single record

        [UseDbContext(typeof(Ticketing))]
        [UseFirstOrDefault]
        [UseProjection]
        [UseFiltering]

        public IQueryable<order> GetOrder([ScopedService] Ticketing context)
        {
            return context.order;
        }




    }
}
