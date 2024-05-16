using Microsoft.EntityFrameworkCore;
using System;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Adapters
{
    public class UserAdapter
    {

        Ticketing db;
        IHttpContextAccessor accessor;
        public UserAdapter(Ticketing _ticketing, IHttpContextAccessor _accessor)
        {
            db = _ticketing;
            accessor = _accessor;
        }

        // Create

        public async Task<user> CreateUser(user user)
        {
            var user_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            user.created_by_ip = user_ip;
            await db.user.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }


        // Read

        public async Task<List<user>> GetUsers()
        {
           
            return await db.user.ToListAsync();
            
        }

        public async Task<List<user>> GetUser(Guid guid)
        {
        
            return await db.user.Where(x => x.guid.Equals(guid) && !x._deleted).ToListAsync();
           
        }

        // Update

        public async Task<List<user>> RetrieveDeletedUser(Guid guid, string first_name)
        {
            var usersToRetrieve = await db.user
                                .Where(x => x.guid.Equals(guid) && x.first_name.Contains(first_name) && x._deleted)
                                .ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            
            foreach (var userToRetrieve in usersToRetrieve)
            {
                userToRetrieve._deleted = false;
                userToRetrieve.modified_by = Guid.NewGuid();
                userToRetrieve.modified_by_ip = modified_by_ip;
                userToRetrieve.date_modified = DateTime.UtcNow;


            }

            await db.SaveChangesAsync();
            return usersToRetrieve;
        }

        public async Task<List<user>> UpdateUser(Guid guid, string first_name, string last_name, string first_name_update, string last_name_update)
        {
            var usersToUpdate = await db.user
                                .Where(x => x.guid.Equals(guid) && x.first_name.Contains(first_name) && x.last_name.Contains(last_name) && !x._deleted)
                                .ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var userToUpdate in usersToUpdate)
            {
                userToUpdate.first_name = first_name_update;
                userToUpdate.last_name = last_name_update;
                userToUpdate.modified_by = Guid.NewGuid();
                userToUpdate.modified_by_ip = modified_by_ip;
                userToUpdate.date_modified = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();
            return usersToUpdate;
        }




        // Delete

        public async Task<List<user>> DeleteUser(Guid guid)
        {
            var usersToDelete = await db.user
                                .Where(x => x.guid.Equals(guid) && !x._deleted)
                                .ToListAsync();
            var modified_by_ip = accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            foreach (var userToDelete in usersToDelete)
            {
                userToDelete._deleted = true;
                userToDelete.modified_by_ip = modified_by_ip;
                userToDelete.date_modified = DateTime.UtcNow;
            }

            await db.SaveChangesAsync();
            return usersToDelete;
        }















    }
}
