namespace Customers.Application.Queries
{
    using Customers.Application.Dto;
    using Customers.Application.Interface;
    using Dapper;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Query list of customers
    /// </summary>
    public class CustomerQuery : Query<QueryCustomerRequest, QueryCustomerResponse>
    {
        public CustomerQuery(ILogger<CustomerQuery> logger, IDbConnection dbConnection)
            : base(logger, dbConnection)
        {
        }

        public override async Task<QueryCustomerResponse> Handle(QueryCustomerRequest request, CancellationToken cancellationToken)
        {
            this.Logger.LogInformation($"Querying customers with name '{request.Name}'");
            string sql = "select * from customers where name like @name";
            IEnumerable<CustomerDto> items = await this.DbConnection.QueryAsync<CustomerDto>(sql, new { name = $"%{request.Name?.Trim()}%" });
            return new QueryCustomerResponse(items.ToArray());
        }
    }
}