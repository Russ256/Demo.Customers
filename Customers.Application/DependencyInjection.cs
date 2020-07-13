namespace Customers.Application
{
    using Customers.Application.Behaviors;
    using Customers.Application.Interface;
    using Customers.Application.Validators;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;

    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomersApplication(this IServiceCollection services)
        {
            // MedaitR
            services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly);

            // MedaitR request pipeline - In order of execution!
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

            // Validators
            services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerValidator>();
            services.AddTransient<IValidator<UpdateCustomerRequest>, UpdateCustomerValidator>();

            return services;
        }
    }
}