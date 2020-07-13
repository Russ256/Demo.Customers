namespace Customers.Domain.Common
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task BeginAsync();

        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        void Rollback();
    }
}