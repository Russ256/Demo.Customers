namespace Customers.Application.Validators
{
    using Customers.Application.Interface;
    using FluentValidation;

    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerValidator()
        {
            this.RuleFor(r => r.Name).NotEmpty().MaximumLength(50);
        }
    }
}