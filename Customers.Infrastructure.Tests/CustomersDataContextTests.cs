namespace Customers.Infrastructure.Tests
{
    using Customers.Domain.Model;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class CustomersDataContextTests
    {
        /// <summary>
        /// Simple test to prove ef context is valid.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ContextIsValid()
        {
            DbContextOptions<CustomersDataContext> options = new DbContextOptionsBuilder<CustomersDataContext>()
                        .UseSqlServer(@"Server=.;Database=Customers;Trusted_Connection=True")
                        .Options;

            CustomersDataContext sut = new CustomersDataContext(options);

            Customer customer = await sut.Customers.FirstOrDefaultAsync();
        }
    }
}