namespace Customers.Application.UnitTests
{
    using Microsoft.Extensions.Logging;
    using Moq;

    public static class MockHelpers
    {
        public static ILogger<T> GetLogger<T>()
        {
            return new Mock<ILogger<T>>().Object;
        }
    }
}