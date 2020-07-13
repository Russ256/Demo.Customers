namespace Customers.Infrastructure
{
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using Microsoft.EntityFrameworkCore;

    public class CustomersDataContext : DataContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDataContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}