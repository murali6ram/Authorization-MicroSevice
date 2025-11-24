using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnitOfWork.Models;

namespace IdentityModel.Models;

/// <summary>
/// Otp Manager Model
/// </summary>
[Table("OtpManager")]
public class OtpManager : PerformedUsers
{
    /// <summary>
    /// Id Property
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// IdentityUserId Property
    /// </summary>
    [Required]
    [ForeignKey(nameof(IdentityUser))]
    public Guid IdentityUserId { get; set; }

    /// <summary>
    /// OneTimePassword Property
    /// </summary>
    [Required]
    public int OneTimePassword { get; set; }

    /// <summary>
    /// OTP creation timestamp
    /// </summary>
    public new DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indicates whether OTP is active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Computed property — true if OTP is still valid (less than 5 minutes old)
    /// </summary>
    [NotMapped]
    public bool IsValid => IsActive && DateTime.UtcNow < CreatedOn.AddMinutes(5);

    /// <summary>
    /// Navigation property
    /// </summary>
    public virtual IdentityUser? IdentityUser { get; set; }
}
