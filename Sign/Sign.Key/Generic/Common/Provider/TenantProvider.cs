using Common.BusinessLogic.Abstractions;
using Common.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Common.Provider;

public class TenantProvider(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IEnvironmentPasswords environmentPasswords)
{
    private readonly IEnvironmentPasswords EnvironmentPasswords = environmentPasswords;

    public const string REPLACE_TEXT = "{database}";
    public const string PASSWORD_TEXT = "{password}";
    public const string ONPREM = "ONPREM";
    public const string SASS = "SASS";

    public string GetTenantDatabaseConnectionString(string databaseName)
    {
        // UNCOMMENT THE BELOW CODE ONLY FOR MIGRATIONS
        // return "Host=acs-hash-dev-rds.cde1axahuad7.us-east-2.rds.amazonaws.com;Port=5432;Username=administrator;Password=c2p0OWDdO8sI;Database=Sample;MinPoolSize=5;MaxPoolSize=100;ConnectionLifetime=0;";

        var rawConnection = configuration.GetConnectionString("ConnectionString")!;
        rawConnection = rawConnection.Replace(PASSWORD_TEXT, EnvironmentPasswords.DbPassword);

        if (configuration["EnvironmentType"] == ONPREM)
            return rawConnection.Replace(REPLACE_TEXT, databaseName);
        try
        {
            var host = httpContextAccessor.HttpContext!.Request.Host.Host;
            var tenant = host.IndexOf(".") > 0 ?
                host.Split('.')[0].RemoveSpecialCharactersAndWhitespace().ToUpper() :
                string.Empty;

            return rawConnection.Replace(REPLACE_TEXT, tenant + databaseName);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unable to fetch th tenant information, check the 'EnvironmentType' in appsettings.json file. Please contact the administrator for more guidance", ex);
        }
    }
}
