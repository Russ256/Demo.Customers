namespace Customers.Application.Commands
{
    using Customers.Application.Dto;
    using Customers.Application.Interface;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class QueryCustomerCommand : Command<QueryCustomerRequest, QueryCustomerResponse>
    {
        private readonly IDbConnection dbConnection;

        public QueryCustomerCommand(IDbConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public override async Task<QueryCustomerResponse> Handle(QueryCustomerRequest request, CancellationToken cancellationToken)
        {
            string sql = "select * from customers where name like @name";

            IEnumerable<CustomerDto> items = await this.dbConnection.QueryAsync<CustomerDto>(sql, new { name = $"%{request.Name?.Trim()}%" });

            return new QueryCustomerResponse(items.ToArray());
        }
    }
}