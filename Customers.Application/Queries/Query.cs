namespace Customers.Application.Queries
{
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Base class for all queries
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public abstract class Query<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Query(ILogger logger, IDbConnection dbConnection)
        {
            this.Logger = logger;
            this.DbConnection = dbConnection;
        }

        protected ILogger Logger { get; }
        protected IDbConnection DbConnection { get; }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}