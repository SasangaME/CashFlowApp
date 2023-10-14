using CashFlowApp.BusinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlowApp.BusinessLogic.Configs;

public static class ServiceResolution
{
    public static IServiceCollection AddBlServices(this IServiceCollection services)
    {
        return services
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<ITodoService, TodoService>()
            .AddScoped<IRoleService, RoleService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>();
    }
}