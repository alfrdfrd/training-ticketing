using Microsoft.EntityFrameworkCore;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Adapters
{
    public class OrderAdapter
    {


        Ticketing db;
        IHttpContextAccessor accessor;
            
        public OrderAdapter(Ticketing _ticketing, IHttpContextAccessor _accessor)
        {
            db = _ticketing;
            accessor = _accessor;
        }

        // Create

        public async Task<order> CreateOrder(order order)
        {
            var order_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            order.created_by_ip = order_ip;
            order.price *= order.quantity;
            await db.order.AddAsync(order);
            await db.SaveChangesAsync();
            return order;
        }


        // Read

        public async Task<List<order>> GetOrders()
        {
            return await db.order.ToListAsync();
        }

        public async Task<List<order>> GetOrder(Guid guid)
        {
            return await db.order.Where(x=> x.guid.Equals(guid) && !x._deleted).ToListAsync();
        }


        // Update

        public async Task<List<order>> UpdateOrder(Guid guid, Guid u_guid, Guid t_guid, int n_quantity, int n_price)
        {
            var orderToUpdate = await db.order.Where(x => x.guid.Equals(guid) && x.user_guid.Equals(u_guid) && x.ticket_guid.Equals(t_guid) && !x._deleted).ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var order in orderToUpdate)
            {
                order.quantity = n_quantity;
                order.price = n_price;

                order.date_modified = DateTime.UtcNow;
                order.modified_by = Guid.NewGuid();
                order.modified_by_ip = modified_by_ip;
            }

            await db.SaveChangesAsync();
            return orderToUpdate;
        }
        


        public async Task<List<order>> RetrieveDeletedOrder(Guid guid)
        {
            var orderToRetrieve = await db.order.Where(x => x.guid.Equals(guid) && x._deleted).ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var order in orderToRetrieve)
            {
                order._deleted = false;
                order.date_modified = DateTime.UtcNow;
                order.modified_by = Guid.NewGuid();
                order.modified_by_ip = modified_by_ip;
            }


            await db.SaveChangesAsync();

            return orderToRetrieve;

        }


        // Delete

        public async Task<List<order>> DeleteOrder(Guid guid)
        {
            var orderToDelete = await db.order
                .Where(x => x.guid.Equals(guid) && !x._deleted)
                .ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var order in orderToDelete)
            {
                order._deleted = true;
                order.date_modified = DateTime.UtcNow;
                order.modified_by = Guid.NewGuid();
                order.modified_by_ip = modified_by_ip;
            }


            await db.SaveChangesAsync();

            return orderToDelete;
        }




    }
}
