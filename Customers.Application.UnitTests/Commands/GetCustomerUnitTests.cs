namespace Customers.Application.UnitTests
{
    using Customers.Application.Commands;
    using Customers.Application.Interface;
    using Customers.Domain.Model;
    using DataRepositoryCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class GetCustomerUnitTests
    {
        [TestMethod]
        public async Task TestSuccess()
        {
            // Arrange
            Guid id = new Guid("b7c2acaa-ad72-47b3-b858-26357cf14fbb");
            string name = "Unit Test";
            Customer customer = new Customer() { Id = id, Name = name };
            Mock<IDataRepository<Customer, Guid>> mockRepository = new Mock<IDataRepository<Customer, Guid>>();
            mockRepository.Setup(m => m.FindAsync(id, default)).ReturnsAsync(customer);
            GetCustomerRequest request = new GetCustomerRequest() { Id = id};
            GetCustomerCommand sut = new GetCustomerCommand(mockRepository.Object);

            // Act
            GetCustomerResponse result = await sut.Handle(request, default);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Customer);
            Assert.AreEqual(name, result.Customer.Name);
            mockRepository.VerifyAll();
            mockRepository.Verify(m => m.FindAsync(id,default), Times.Once);
        }


        [TestMethod]
        public async Task TestNotFound()
        {
            // Arrange
            Mock<IDataRepository<Customer, Guid>> mockRepository = new Mock<IDataRepository<Customer, Guid>>();
            Guid id = new Guid("b7c2acaa-ad72-47b3-b858-26357cf14fbb");
            mockRepository.Setup(m => m.FindAsync(id, default)).Returns(new ValueTask<Customer>((Customer)null));
            GetCustomerRequest request = new GetCustomerRequest() { Id = id };
            GetCustomerCommand sut = new GetCustomerCommand(mockRepository.Object);

            // Act
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await sut.Handle(request, default));

            // Assert
            mockRepository.VerifyAll();
            mockRepository.Verify(m => m.FindAsync(id, default), Times.Once);
        }
    }
}