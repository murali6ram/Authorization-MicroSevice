using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UnitOfWork.Models;

namespace IdentityModel.Models;

/// <summary>
/// Identity User Model
/// </summary>
[Table("IdentityUsers")]
public class IdentityUser: PerformedUsers
{
    /// <summary>
    /// Id Property
    /// </summary>
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    /// <summary>
    /// Email Property
    /// </summary>
    [Required, EmailAddress, MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// PhoneNumber Property
    /// </summary>
    [Required, Phone, MaxLength(256)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// UserName Property
    /// </summary>
    [Required, MaxLength(256)]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Name Property
    /// </summary>
    [Required, MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Navigation property — all OTPs for this user
    /// </summary>
    public virtual ICollection<OtpManager>? OtpManagers { get; set; }
}
