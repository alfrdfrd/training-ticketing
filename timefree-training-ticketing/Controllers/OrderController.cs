using Microsoft.AspNetCore.Mvc;
using System.Net;
using timefree_training_ticketing.Adapters;
using timefree_training_ticketing.Models.EF.Ticketing;

namespace timefree_training_ticketing.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        
        OrderAdapter orderAdapter;

        public OrderController(OrderAdapter _orderAdapter)
        {
            orderAdapter = _orderAdapter;
        }


        [HttpPost]
        [ProducesResponseType(typeof(order), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder([FromBody] order order)
        {
            try
            {
                return Ok(await orderAdapter.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                return Ok(await orderAdapter.GetOrders());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrder(Guid guid)
        {
            try
            {
                return Ok(await orderAdapter.GetOrder(guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(order), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrder(Guid guid, Guid u_guid, Guid t_guid, int n_quantity, int n_price)
        {
            try
            {
                return Ok(await orderAdapter.UpdateOrder(guid, u_guid, t_guid, n_quantity, n_price));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(order), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RetrieveDeletedOrder(Guid guid)
        {
            try
            {
                return Ok(await orderAdapter.RetrieveDeletedOrder(guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(order), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(Exception), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTicket([FromBody] order order)
        {
            try
            {
                return Ok(await orderAdapter.DeleteOrder(order.guid));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
