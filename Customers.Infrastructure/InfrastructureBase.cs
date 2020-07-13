namespace Customers.Infrastructure
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Base class for infrastructure classes.
    /// </summary>
    public abstract class InfrastructureBase
    {
        protected readonly ILogger Logger;

        public InfrastructureBase(ILogger logger)
        {
            this.Logger = logger;
        }
    }
}