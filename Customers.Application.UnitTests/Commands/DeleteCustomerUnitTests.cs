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
    public class DeleteCustomerUnitTests
    {
        [TestMethod]
        public async Task TestSuccess()
        {
            // Arrange
            Mock<IDataRepository<Customer, Guid>> mockRepository = new Mock<IDataRepository<Customer, Guid>>();
            Guid id = new Guid("b7c2acaa-ad72-47b3-b858-26357cf14fbb");
            Customer customer = new Customer() { Id = id };
            mockRepository.Setup(m => m.FindAsync(id, default)).Returns(new ValueTask<Customer>(customer));
            DeleteCustomerRequest request = new DeleteCustomerRequest() { Id = id };
            DeleteCustomerCommand sut = new DeleteCustomerCommand(MockHelpers.GetLogger<DeleteCustomerCommand>(), mockRepository.Object);

            // Act
            DeleteCustomerResponse result = await sut.Handle(request, default);

            // Assert
            Assert.IsNotNull(result);
            mockRepository.VerifyAll();
            mockRepository.Verify(m => m.FindAsync(id, default), Times.Once);
            mockRepository.Verify(m => m.Delete(customer), Times.Once);
        }

        [TestMethod]
        public async Task TestNotFound()
        {
            // Arrange
            Mock<IDataRepository<Customer, Guid>> mockRepository = new Mock<IDataRepository<Customer, Guid>>();
            Guid id = new Guid("b7c2acaa-ad72-47b3-b858-26357cf14fbb");
            mockRepository.Setup(m => m.FindAsync(id, default)).Returns(new ValueTask<Customer>((Customer)null));
            DeleteCustomerRequest request = new DeleteCustomerRequest() { Id = id };
            DeleteCustomerCommand sut = new DeleteCustomerCommand(MockHelpers.GetLogger<DeleteCustomerCommand>(), mockRepository.Object);

            // Act
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () => await sut.Handle(request, default));

            // Assert
            mockRepository.VerifyAll();
            mockRepository.Verify(m => m.FindAsync(id, default), Times.Once);
            mockRepository.Verify(m => m.Delete(It.IsAny<Customer>()), Times.Never);
        }
    }
}