using Microsoft.AspNetCore.Mvc;
using System.Net;
using timefree_training_ticketing.Adapters;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        UserAdapter userAdapter;
        public UserController(UserAdapter _userAdapter) 
        { 
            userAdapter = _userAdapter;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<user>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                return Ok(await userAdapter.GetUsers());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<user>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(Guid guid)
        {
            try
            {
                return Ok(await userAdapter.GetUser(guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(user), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUser([FromBody] user user)
        {
            try
            {
                return Ok(await userAdapter.CreateUser(user));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(user), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveDeletedUser(Guid guid, string first_name)
        {
            try
            {
                return Ok(await userAdapter.RetrieveDeletedUser(guid, first_name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(user), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(Guid guid, string first_name, string last_name, string first_name_update, string last_name_update)
        {
            try
            {
                return Ok(await userAdapter.UpdateUser(guid, first_name, last_name, first_name_update, last_name_update));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [ProducesResponseType(typeof(user), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromBody] user user)
        {
            try
            {
                return Ok(await userAdapter.DeleteUser(user.guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}
