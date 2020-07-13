namespace Customers.Infrastructure
{
    using Customers.Domain.Common;
    using DataRepositoryCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Logging;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Concrete implementation of unit of work for a single IDataContext.
    /// </summary>
    public class UnitOfWork : InfrastructureBase, IUnitOfWork
    {
        private readonly IDataContext dataContext;

        public UnitOfWork(ILogger<UnitOfWork> logger, IDataContext dataContext)
            : base(logger)
        {
            this.dataContext = dataContext;
        }

        public Task BeginAsync()
        {
            // Nothing to do here.

            return Task.CompletedTask;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            int result = 0;

            IExecutionStrategy strategy = this.dataContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                IDbContextTransaction transaction = await this.dataContext.Database.BeginTransactionAsync();

                result = await this.dataContext.SaveChangesAsync(cancellationToken);

                // Note: we could also save changes in other db contexts here to participate in the transaction.

                transaction.Commit();
            });

            return result;
        }

        public void Rollback()
        {
            // Nothing to do here as nothing will have been committed to the db.
        }
    }
}