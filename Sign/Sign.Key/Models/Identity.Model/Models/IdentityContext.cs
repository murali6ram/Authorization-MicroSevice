using Microsoft.EntityFrameworkCore;

namespace IdentityModel.Models;

public class IdentityContext: DbContext
{
    /// <summary>
    /// Parameterless Constructor for unit testing
    /// </summary>
    public IdentityContext()
    : base() { }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">DbContextOptions</param>
    public IdentityContext(DbContextOptions<IdentityContext> options)
    : base(options) { }

    /// <summary>
    /// OnModelCreating method over ridded [NOTE :: One to Many format]
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        this.OnModelSeeding(modelBuilder);
    }

    /// <summary>
    /// OnModelSeeding method
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder</param>

    private void OnModelSeeding(ModelBuilder modelBuilder)
    {
        var identityUserId = new  Guid("1234abcd-1234-1234-1234-ab1234567890");
        var otpId = new Guid("abcd1234-abcd-1234-abcd-1234567890ef");
        var email = "superuser@email.com";
        var name = "Super User";
        var phoneNumber = "1234";
        long createdOn = 1763818667L;

        IdentityUser identityUser = new()
        {
            Id = identityUserId,
            Email = email,
            Name = name,
            PhoneNumber = phoneNumber,
            CreatedBy = name,
            ModifiedBy = name,
            CreatedOn = createdOn,
            ModifiedOn = createdOn
        };

        OtpManager otpManager = new()
        {
            Id = otpId,
            OneTimePassword = 1234,
            CreatedBy = name,
            ModifiedBy = name,
            CreatedOn = DateTimeOffset.FromUnixTimeMilliseconds(createdOn).UtcDateTime,
            ModifiedOn = createdOn,
            IdentityUserId = identityUserId
        };
        modelBuilder.Entity<IdentityUser>().HasData(identityUser);
        modelBuilder.Entity<OtpManager>().HasData(otpManager);
    }

    #region Entities

    /// <summary>
    /// Identity Users DbSet
    /// </summary>
    public DbSet<IdentityUser> IdentityUsers { get; set; }

    /// <summary>
    /// Otp Manager DbSet
    /// </summary>
    public DbSet<OtpManager> OtpManager { get; set; }

    #endregion
}
