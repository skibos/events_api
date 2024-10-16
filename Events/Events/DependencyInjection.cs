using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Events.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        //swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}