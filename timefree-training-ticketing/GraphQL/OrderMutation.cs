using System.Net;
using timefree_training_ticketing.Models.Classes;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.GraphQL
{
    public partial class Mutation
    {


        public async Task<OrderResponse> CreateOrder
            (order input, [ScopedService] Ticketing db, CancellationToken cancellationToken)
        {
            var order_created_by = Guid.NewGuid();
            var order_created = DateTime.UtcNow;
            var order_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    input.created_by = order_created_by;
                    input.created_by_ip = order_ip;
                    input.date_created = order_created;
                    input.price *= input.quantity;
                    await db.AddAsync(input);
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new OrderResponse
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Order Created",
                        Order = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new OrderResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Creation!",
                        ResponseMessage = e.Message
                    };

                }

            }
        }

        public async Task<OrderResponse> UpdateOrder(
            order input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var order_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var existingOrder = await db.order.FindAsync(input.guid);
                    if (existingOrder == null)
                    {
                        return new OrderResponse
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "Order not found",
                            ResponseMessage = $"Order with ID {input.guid} not found"
                        };
                    }
                    existingOrder.user_guid = input.user_guid;
                    existingOrder.ticket_guid = input.ticket_guid;
                    existingOrder.quantity = input.quantity;
                    existingOrder.price = input.price * input.quantity;
                    existingOrder.modified_by = Guid.NewGuid();
                    existingOrder.modified_by_ip = order_ip;
                    existingOrder.date_modified = DateTime.UtcNow;
                    existingOrder._deleted = input._deleted;

                    await db.SaveChangesAsync(cancellationToken);
                    await tx.CommitAsync();
                    return new OrderResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Order Modified!",
                        Order = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new OrderResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Modification",
                        ResponseMessage = e.Message
                    };

                }
            }

        }












        public async Task<OrderResponse> DeleteOrder(
            order input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var order_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {

                    var existingOrder = await db.order.FindAsync(input.guid);
                    if (existingOrder == null)
                    {
                        return new OrderResponse()
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "Order not found",
                            ResponseMessage = $"Order with ID {input.guid} not found"
                        };
                    }
                    existingOrder.modified_by = Guid.NewGuid();
                    existingOrder.modified_by_ip = order_ip;
                    existingOrder.date_modified = DateTime.UtcNow;
                    existingOrder._deleted = true;
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new OrderResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "Order Deleted",
                        Order = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new OrderResponse()
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
