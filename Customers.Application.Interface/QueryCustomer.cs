namespace Customers.Application.Interface
{
    using Customers.Application.Dto;
    using MediatR;

    public class QueryCustomerRequest : IRequest<QueryCustomerResponse>
    {
        public string Name { get; set; }
    }

    public class QueryCustomerResponse
    {
        public CustomerDto[] Customers { get; private set; }

        public QueryCustomerResponse(CustomerDto[] customers)
        {
            this.Customers = customers;
        }
    }
}