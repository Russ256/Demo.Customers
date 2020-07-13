﻿namespace Customers.Application.Commands
{
    using Customers.Application.Interface;
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Deletes a customer
    /// </summary>
    public class DeleteCustomerCommand : Command<DeleteCustomerRequest, DeleteCustomerResponse>
    {
        private readonly IDataRepository<Customer, Guid> repository;

        public DeleteCustomerCommand(ILogger<DeleteCustomerCommand> logger, IDataRepository<Customer, Guid> repository)
            : base(logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<DeleteCustomerResponse> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            Customer customer = await this.repository.FindAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException();
            }
            this.repository.Delete(customer);

            return new DeleteCustomerResponse();
        }
    }
}