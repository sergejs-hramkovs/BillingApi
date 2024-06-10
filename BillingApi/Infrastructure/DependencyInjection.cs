using Billing.Services.Implementation;
using Billing.Services.Interfaces;
using Services.Interfaces;
using System.Reflection;

namespace Services.Infrastructure
{
    public static class DependencyInjection
    {
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

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IReceiptService, ReceiptService>();

            return services;
        }
    }
}
