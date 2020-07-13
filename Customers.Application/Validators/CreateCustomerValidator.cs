namespace Customers.Application.Validators
{
    using Customers.Application.Interface;
    using FluentValidation;

    /// <summary>
    /// Customer Validator
    /// </summary>
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
    {
        public CreateCustomerValidator()
        {
            this.RuleFor(r => r.Name).NotEmpty().MaximumLength(50);
        }
    }
}