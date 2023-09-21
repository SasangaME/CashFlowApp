using CashFlowApp.BusinessLogic.Configs;
using CashFlowApp.Repositories.Configs;

namespace CashFlowApp.API.Configs;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddConfigurationServices(this IServiceCollection services)
    {
        return services
            .AddRepositories()
            .AddBlServices();
    }
}