using Common.Provider;
using IdentityModel.Models;
using IdentityServer.Business.DataAccess.Abstarctions;
using IdentityServer.BusinessAccess.Abstarctions;
using Microsoft.EntityFrameworkCore;
using Common.Extension;
using IdentityProvider.Controllers.Token;

namespace IdentityServer.Business.Extensions;

public static class StartupExtensions
{
    #region Services
    /// <summary>
    /// Configure Services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <param name="configuration">IConfiguration</param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTenantProvider();
        services.AddDbContext<IdentityContext>((serviceProvider, options) =>
        {
            var tenantProvider = serviceProvider.GetRequiredService<TenantProvider>();
            options.UseNpgsql(tenantProvider.GetTenantDatabaseConnectionString(UnitOfWork.Constants.Databases.IDS_DATABASE));
        });
        services.AddScopes();
        services.AddControllers();

        return services;
    }

    /// <summary>
    /// Configure Scoped Services
    /// </summary>
    /// <param name="services">IServiceCollection</param>
    /// <returns>IServiceCollection</returns>
    private static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, DataAccess.UnitOfWork>();
        services.AddScoped<IBusinessAccess, BusinessAccess.BusinessAccess>();
        services.AddScoped<TokenController>();
        return services;
    }
    #endregion
}
