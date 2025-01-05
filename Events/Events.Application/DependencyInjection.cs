using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Events.Application.Common.Interfaces.Workers;

namespace Events.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped<EventWorker>();

        return services;
    }
}