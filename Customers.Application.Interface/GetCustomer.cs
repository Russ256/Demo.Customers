namespace Customers.Application.Interface
{
    using Customers.Application.Dto;
    using MediatR;
    using System;

    public class GetCustomerRequest : IRequest<GetCustomerResponse>
    {
        public Guid Id { get; set; }
    }

    public class GetCustomerResponse
    {
        public CustomerDto Customer { get; private set; }

        public GetCustomerResponse(CustomerDto customerDto)
        {
            this.Customer = customerDto;
        }
    }
}