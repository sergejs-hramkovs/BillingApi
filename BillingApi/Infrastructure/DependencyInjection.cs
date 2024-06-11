using API.Validators;
using Billing.Data.Dto;
using Billing.Services.Implementation;
using Billing.Services.Interfaces;
using FluentValidation;
using Serilog;
using Services.Implementation;
using Services.Interfaces;
using System.Reflection;

namespace Services.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the implementations of the IPaymentGatewayStrategy interface from the specified assembly to the service collection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the strategies to.</param>
        /// <param name="assembly">The assembly to search for strategy implementations.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection AddStrategies(this IServiceCollection services, Assembly assembly)
        {
            IEnumerable<Type> strategies = assembly
                .GetTypes()
                .Where(x => typeof(IPaymentGatewayStrategy).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface);

            foreach (Type strategy in strategies)
            {
                services.AddScoped(typeof(IPaymentGatewayStrategy), strategy);
            }

            return services;
        }

        /// <summary>
        /// Registers the services and validators to the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the services and validators are to be added.</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IUserCheckingService, UserCheckingService>();
            services.AddSingleton(Log.Logger);

            // Validators
            services.AddScoped<IValidator<OrderInputDto>, OrderInputValidator>();

            return services;
        }
    }
}
