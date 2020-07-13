namespace Customers.Application.UnitTests.Validators
{
    using Customers.Application.Interface;
    using Customers.Application.Validators;
    using FluentValidation.Results;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CreateCustomerValidatorUnitTests
    {
        [TestMethod]
        public void NameTooLong()
        {
            // Arrange
            CreateCustomerRequest request = new CreateCustomerRequest() { Name = "123456789012345678901234567890123456789012345678901234567890" };
            CreateCustomerValidator sut = new CreateCustomerValidator();

            // Act
            ValidationResult result = sut.Validate(request);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Errors.Count);
            Assert.AreEqual("MaximumLengthValidator", result.Errors[0].ErrorCode);
            Assert.AreEqual("Name", result.Errors[0].PropertyName);
        }
    }
}