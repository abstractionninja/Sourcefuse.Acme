using Microsoft.AspNetCore.Mvc;
using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Sourcefuse.Acme.WebApi.Controllers
{
    /// <summary>
    /// Controller for orders 
    /// <seealso cref="IOrder"/>
    /// </summary>
    /// <param name="repoService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IAsyncRepoService repoService) : ControllerBase
    {
        private readonly IAsyncRepoService _repoService = repoService;

        /// <summary>
        /// Get list of Orders
        /// </summary>
        /// <returns code="200">Enumerable list of orders <seealso cref="IOrder"/></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(IEnumerable<IOrder>))]
        public async Task<IEnumerable<IOrder>> Get()
            => await _repoService.GetOrders();

        /// <summary>
        /// Get list of Orders by customer id
        /// </summary>
        /// <param name="customerId">Unique customer identifier</param>
        /// <returns code="200">Enumerable list of orders <seealso cref="IOrder"/></returns>
        /// <returns code="400">Bad Request</returns>
        [HttpGet("bycustomer/{customerId}")]
        [SwaggerResponse(200, Type = typeof(IEnumerable<IOrder>))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> GetByCustomerId(Guid customerId)
        {
            try
            {
                return Ok(await _repoService.GetOrdersForCustomer(customerId));
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Unique order identifies</param>
        /// <returns code="200">Found order <seealso cref="IOrder"/></returns>
        /// <returns code="404">Order not found</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200, Type = typeof(IOrder))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _repoService.GetOrder(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Add new order
        /// </summary>
        /// <param name="order">Order to add</param>
        /// <returns code="200">Newly created order <seealso cref="IOrder"/></returns>
        /// <returns code="404">Customer not found</returns>
        /// <returns code="400">Bad Request</returns>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(IOrder))]
        [SwaggerResponse(400)]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Post([FromBody] OrderModel order)
        {
            try
            {
                var result = await _repoService.AddOrder(order);

                return (result == null) ? BadRequest() : Ok(result);
            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Update an order
        /// </summary>
        /// <param name="order">Updated order data</param>
        /// <param name="orderId">Unique identifier of order to update</param>
        /// <returns code="200">Updated order</returns>
        /// <returns code="404">Order not found</returns>
        /// <returns code="400">Bad Request</returns>
        [HttpPut("update/{orderid}")]
        [SwaggerResponse(200, Type = typeof(IOrder))]
        [SwaggerResponse(404)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Update([FromBody] OrderModel order, Guid orderId)
        {
            try
            {
                var result = await _repoService.UpdateOrder(orderId,order);

                return (result == null) ? BadRequest() : Ok(result);

            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
