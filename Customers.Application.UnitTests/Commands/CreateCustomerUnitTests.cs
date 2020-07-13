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
    public class CreateCustomerUnitTests
    {
        [TestMethod]
        public async Task TestSuccess()
        {
            // Arrange
            Mock<IDataRepository<Customer, Guid>> mockRepository = new Mock<IDataRepository<Customer, Guid>>();
            mockRepository.Setup(m => m.Add(It.IsAny<Customer>()));
            string name = "Unit Test";
            CreateCustomerRequest request = new CreateCustomerRequest() { Name = name };
            CreateCustomerCommand sut = new CreateCustomerCommand(MockHelpers.GetLogger<CreateCustomerCommand>(), mockRepository.Object);

            // Act
            CreateCustomerResponse result = await sut.Handle(request, default);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Customer);
            Assert.AreEqual(name, result.Customer.Name);
            mockRepository.VerifyAll();
            mockRepository.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once);
        }
    }
}