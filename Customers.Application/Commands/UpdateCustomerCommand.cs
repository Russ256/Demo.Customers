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
    /// Update customer details
    /// </summary>
    public class UpdateCustomerCommand : Command<UpdateCustomerRequest, UpdateCustomerResponse>
    {
        private readonly IDataRepository<Customer, Guid> repository;

        public UpdateCustomerCommand(ILogger<UpdateCustomerCommand> logger, IDataRepository<Customer, Guid> repository)
            : base(logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<UpdateCustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer customer = await this.repository.FindAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException();
            }

            customer.Name = request.Name;

            return new UpdateCustomerResponse(new CustomerDto(customer.Id, customer.Name));
        }
    }
}