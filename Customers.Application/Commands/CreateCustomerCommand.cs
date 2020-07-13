namespace Customers.Application.Commands
{
    using Customers.Application.Dto;
    using Customers.Application.Interface;
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Create new customer command handler.
    /// </summary>
    public class CreateCustomerCommand : Command<CreateCustomerRequest, CreateCustomerResponse>
    {
        private readonly IDataRepository<Customer, Guid> repository;

        public CreateCustomerCommand(IDataRepository<Customer, Guid> repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override Task<CreateCustomerResponse> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer newEntity = new Customer()
            {
                Name = request.Name
            };

            this.repository.Add(newEntity);

            return Task.FromResult(new CreateCustomerResponse(new CustomerDto(newEntity.Id, newEntity.Name)));
        }
    }
}