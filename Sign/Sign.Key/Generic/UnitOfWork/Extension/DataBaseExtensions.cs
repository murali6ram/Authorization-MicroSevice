using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;

namespace UnitOfWork.Extension
{
    public static class DataBaseExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<T>().Database.Migrate();

            return host;
        }

        public static WebApplication MigrateDatabase<T>(this WebApplication app) where T : DbContext
        {
            using (var scope = app.Services.CreateScope())
                scope.ServiceProvider.GetRequiredService<T>().Database.Migrate();

            return app;
        }
    }
}
