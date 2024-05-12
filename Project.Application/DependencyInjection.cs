using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Common;
using MediatR.NotificationPublishers;
namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.NotificationPublisher = new ForeachAwaitPublisher();
            });
            return services;
        }
    }
}
