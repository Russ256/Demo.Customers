namespace Customers.Application.Behaviors
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Runs the registered validators for the request before continuing the pipeline
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ValidationBehavior<TRequest, TResponse> : BehaviorBase, IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger, IEnumerable<IValidator<TRequest>> validators)
            : base(logger)
        {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            this.Logger.LogTrace("Running validations...");

            List<ValidationResult> results = new List<ValidationResult>();

            // Run the synchronous validators
            foreach (IValidator<TRequest> validator in this.validators)
            {
                results.Add(validator.Validate(request));
            }

            // Check for errors
            List<ValidationFailure> failures = results
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                this.Logger.LogTrace("Failed validation");
                throw new ValidationException(failures);
            }

            this.Logger.LogTrace("Validation passed");
            TResponse response = await next();
            return response;
        }
    }
}