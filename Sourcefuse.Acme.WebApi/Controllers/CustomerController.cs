using Microsoft.AspNetCore.Mvc;
using Sourcefuse.Acme.Common.Interfaces;
using Sourcefuse.Acme.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Sourcefuse.Acme.WebApi.Controllers
{
    /// <summary>
    /// Controller for customers 
    /// <seealso cref="ICustomer"/>
    /// </summary>
    /// <param name="repoService"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController(IAsyncRepoService repoService) : ControllerBase
    {

        private readonly IAsyncRepoService _repoService = repoService;

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <returns code="200">Enumerable list of customers <seealso cref="ICustomer"/></returns>
        [HttpGet]
        [SwaggerResponse(200, Type = typeof(IEnumerable<ICustomer>))]
        public async Task<IEnumerable<ICustomer>> Get()
            => await _repoService.GetCustomers();

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">Unique customer identifier</param>
        /// <returns code="200">Found customer <seealso cref="ICustomer"/></returns>
        /// <returns code="404">Not found</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(200,Type = typeof(ICustomer))]
        [SwaggerResponse(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _repoService.GetCustomer(id);

            return (result == null) ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="customer">Customer to add</param>
        /// <returns code="200">Newly created customer <seealso cref="ICustomer"/></returns>
        /// <returns code="400">Bad Request</returns>
        [HttpPost]
        [SwaggerResponse(200, Type = typeof(ICustomer))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Post([FromBody] CustomerModel customer)
        {
            var result = await _repoService.AddCustomer(customer);

            return (result == null) ? BadRequest() : Ok(result);
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="customer">Updated customer data</param>
        /// <param name="customerId">Unique identifier of customer to update</param>
        /// <returns code="200">Updated customer <seealso cref="ICustomer"/></returns>
        /// <returns code="404">Customer not found</returns>
        /// <returns code="400">Bad Request</returns>
        [HttpPut("update/{customerId}")]
        [SwaggerResponse(200, Type = typeof(ICustomer))]
        [SwaggerResponse(404)]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Update([FromBody] CustomerModel customer, Guid customerId)
        {
            try
            {
                var result = await _repoService.UpdateCustomer(customerId, customer);

                return (result == null) ? BadRequest() : Ok(result);

            } catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
