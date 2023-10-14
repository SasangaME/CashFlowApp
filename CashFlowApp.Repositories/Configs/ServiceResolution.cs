using CashFlowApp.Repositories.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlowApp.Repositories.Configs;

public static class ServiceResolution
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddScoped<IUserRepository, UserRepository>();
    }
}