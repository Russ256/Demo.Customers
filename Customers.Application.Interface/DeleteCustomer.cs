namespace Customers.Application.Interface
{
    using MediatR;
    using System;

    public class DeleteCustomerRequest : IRequest<DeleteCustomerResponse>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerResponse
    {
    }
}