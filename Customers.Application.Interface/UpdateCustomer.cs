namespace Customers.Application.Interface
{
    using Customers.Application.Dto;
    using MediatR;
    using System;

    public class UpdateCustomerRequest : IRequest<UpdateCustomerResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateCustomerResponse
    {
        public CustomerDto Customer { get; private set; }

        public UpdateCustomerResponse(CustomerDto customerDto)
        {
            this.Customer = customerDto;
        }
    }
}