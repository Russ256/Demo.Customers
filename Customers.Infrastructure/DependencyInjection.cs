namespace Customers.Infrastructure
{
    using Customers.Domain.Common;
    using DataRepositoryCore;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Data;

    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomersInfrastructure(this IServiceCollection services, string connectionString)
        {
            // Infrastructure setup.
            services.AddEntityFrameworkSqlServer()
                    .AddDbContext<IDataContext, CustomersDataContext>(options =>
                    {
                        options.UseSqlServer(connectionString,
                                             s => s.EnableRetryOnFailure());
                    },
                    ServiceLifetime.Scoped
                    );

            // Generic repositories
            services.AddDataRepositories();

            // UOW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDbConnection>((services) =>
            {
                IDbConnection conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            });

            return services;
        }
    }
}