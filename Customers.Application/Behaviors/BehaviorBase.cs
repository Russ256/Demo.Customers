namespace Customers.Application.Behaviors
{
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Base class for MediatR behaviors
    /// </summary>
    public abstract class BehaviorBase
    {
        protected readonly ILogger Logger;

        public BehaviorBase(ILogger logger)
        {
            this.Logger = logger;
        }
    }
}