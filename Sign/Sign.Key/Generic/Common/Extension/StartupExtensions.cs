using Common.BusinessLogic;
using Common.BusinessLogic.Abstractions;
using Common.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extension;

public static class StartupExtension
{
    public static IServiceCollection AddTenantProvider(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSingleton<TenantProvider>();
        services.AddSingleton<IEnvironmentPasswords, EnvironmentPasswords>();
        return services;
    }
}
