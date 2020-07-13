namespace Customers.Application.Commands
{
    using Customers.Application.Dto;
    using Customers.Application.Interface;
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates new customer.
    /// </summary>
    public class CreateCustomerCommand : Command<CreateCustomerRequest, CreateCustomerResponse>
    {
        private readonly IDataRepository<Customer, Guid> repository;

        public CreateCustomerCommand(ILogger<CreateCustomerCommand> logger, IDataRepository<Customer, Guid> repository)
            : base(logger)
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