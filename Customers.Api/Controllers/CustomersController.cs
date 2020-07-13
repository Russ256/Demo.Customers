namespace Customers.Api.Controllers
{
    using AutoMapper;
    using Customers.Api.Dto;
    using Customers.Application.Interface;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Customer controller
    /// </summary>
    [Route("api/v1.0/customer")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CustomersController : ApiControllerBase
    {
        public CustomersController(IMediator mediator, IMapper mapper)
            : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets a customers details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            return await this.ExecuteCommandAsync(new GetCustomerRequest() { Id = id }, (GetCustomerResponse response) =>
            {
                return this.Mapper.Map<Dto.CustomerDto>(response.Customer);
            });
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await this.ExecuteCommandAsync(new DeleteCustomerRequest() { Id = id });
        }

        /// <summary>
        /// Update the customers details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id:guid}")]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateCustomerDto request)
        {
            return await this.ExecuteCommandAsync(new UpdateCustomerRequest() { Id = id, Name = request.Name }, (UpdateCustomerResponse response) =>
            {
                return this.Mapper.Map<Dto.CustomerDto>(response.Customer);
            });
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCustomerRequestDto request)
        {
            return await this.ExecuteCommandAsync(new CreateCustomerRequest() { Name = request.Name }, (CreateCustomerResponse response) =>
            {
                return this.Mapper.Map<Dto.CustomerDto>(response.Customer);
            });
        }

        /// <summary>
        /// Get a list of customers matching the query
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(CustomerDto[]), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> QueryAsync([FromQuery] string name)
        {
            return await this.ExecuteCommandAsync(new QueryCustomerRequest() { Name = name }, (QueryCustomerResponse response) =>
            {
                return this.Mapper.Map<Dto.CustomerDto[]>(response.Customers);
            });
        }
    }
}