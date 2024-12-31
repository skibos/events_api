using Events.Application.Common.Interfaces.Persistance;
using Events.Application.Common.Interfaces.Services;
using Events.Infrastructure.Persistance;
using Events.Infrastructure.Persistance.Repositories;
using Events.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Events.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
