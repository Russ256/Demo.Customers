namespace Customers.Application.Interface
{
    using Customers.Application.Dto;
    using MediatR;

    /// <summary>
    /// Command Request
    /// </summary>
    public class CreateCustomerRequest : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// Command Response
    /// </summary>
    public class CreateCustomerResponse
    {
        public CreateCustomerResponse(CustomerDto customerDto)
        {
            this.Customer = customerDto;
        }

        public CustomerDto Customer { get; private set; }
    }
}