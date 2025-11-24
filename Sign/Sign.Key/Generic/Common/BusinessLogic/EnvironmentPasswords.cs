using Common.BusinessLogic.Abstractions;
using Common.Services;

namespace Common.BusinessLogic;

public class EnvironmentPasswords : IEnvironmentPasswords
{
    public string DbPassword { get; private set; }
    public string? MailPassword { get; private set; }

    public EnvironmentPasswords()
    {
        DbPassword = CryptoServices.Decrypt(Environment.GetEnvironmentVariable("PGPASSWORD") ?? throw new InvalidOperationException("Database password environment variable 'PGPASSWORD' is not set"));
        var encryptedEmailPassword = Environment.GetEnvironmentVariable("EMAILPASSWORD");
        MailPassword = string.IsNullOrWhiteSpace(encryptedEmailPassword) ? null : CryptoServices.Decrypt(encryptedEmailPassword);
    }
}
