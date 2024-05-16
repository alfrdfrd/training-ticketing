using System.Net;
using timefree_training_ticketing.Models.Classes;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.GraphQL
{
    public partial class Mutation
    {

        public IHttpContextAccessor accessor;
        public Mutation(IHttpContextAccessor _accessor)
        {
        
            accessor = _accessor;
        }

        public async Task<UserResponse> CreateUser(
            user input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var user_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            var user_guid = Guid.NewGuid();
            var user_date_created = DateTime.UtcNow; 
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                   input.created_by = user_guid;
                   input.created_by_ip = user_ip;
                   input.date_created = user_date_created;
                    await db.AddAsync(input);
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new UserResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "User Created",
                        User = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new UserResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Creation!",
                        ResponseMessage = e.Message
                    };

                }
            }

        }



        public async Task<UserResponse> UpdateUser(
            user input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var user_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var existingUser = await db.user.FindAsync(input.guid);
                    if (existingUser == null)
                    {
                        return new UserResponse
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "User not found",
                            ResponseMessage = $"User with ID {input.guid} not found"
                        };
                    }
                    existingUser.modified_by = Guid.NewGuid();
                    existingUser.modified_by_ip = user_ip;
                    existingUser.date_modified = DateTime.UtcNow;
                    existingUser._deleted = input._deleted;
                    existingUser.first_name = input.first_name;
                    existingUser.last_name = input.last_name;
                    await db.SaveChangesAsync(cancellationToken);
                    await tx.CommitAsync();
                    return new UserResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "User Modified!",
                        User = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new UserResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.BadRequest),
                        ResponseLabel = "Error in Modification",
                        ResponseMessage = e.Message
                    };

                }
            }

        }

        public async Task<UserResponse> DeleteUser(
            user input, [ScopedService] Ticketing db, CancellationToken cancellationToken
            )
        {
            var user_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            using (var tx = await db.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    
                    var existingUser = await db.user.FindAsync(input.guid);
                    if (existingUser == null)
                    {
                        return new UserResponse
                        {
                            ResponseCode = (int)HttpStatusCode.NotFound,
                            ResponseLabel = "User not found",
                            ResponseMessage = $"User with ID {input.guid} not found"
                        };
                    }
                    existingUser.modified_by = Guid.NewGuid();
                    existingUser.modified_by_ip = user_ip;
                    existingUser.date_modified = DateTime.UtcNow;
                    existingUser._deleted = true;
                    var addResponse = await db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return new UserResponse()
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.Accepted),
                        ResponseLabel = "Success!",
                        ResponseMessage = "User Deleted",
                        User = input
                    };
                }
                catch (Exception e)
                {
                    await tx.RollbackAsync();
                    return new UserResponse()
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
