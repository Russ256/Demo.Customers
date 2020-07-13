namespace Customers.Application.Behaviors
{
    using Customers.Domain.Common;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Manages the unit of work the command runs under in the Mediator pipeline.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class UnitOfWorkBehavior<TRequest, TResponse> : BehaviorBase, IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkBehavior(ILogger<UnitOfWorkBehavior<TRequest, TResponse>> logger, IUnitOfWork unitOfWork)
            : base(logger)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                await this.unitOfWork.BeginAsync();
                TResponse response = await next();
                await this.unitOfWork.CommitAsync();

                return response;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, "Unit of work failed.");
                this.unitOfWork.Rollback();
                throw;
            }
        }
    }
}