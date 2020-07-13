namespace Customers.Application.Commands
{
    using Customers.Application.Dto;
    using Customers.Application.Interface;
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCustomerCommand : Command<GetCustomerRequest, GetCustomerResponse>
    {
        private readonly IReadDataRepository<Customer, Guid> repository;

        public GetCustomerCommand(IReadDataRepository<Customer, Guid> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<GetCustomerResponse> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer customer = await this.repository.FindAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            return new GetCustomerResponse(new CustomerDto(customer.Id, customer.Name));
        }
    }
}