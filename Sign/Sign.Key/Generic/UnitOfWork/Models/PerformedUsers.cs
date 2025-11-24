using System.ComponentModel.DataAnnotations;

namespace UnitOfWork.Models;

public class PerformedUsers: Statuses
{
    /// <summary>
    /// Created By Property
    /// </summary>
    [Required]
    public string CreatedBy { get; set; } = string.Empty;

    /// <summary>
    /// Modified By Property
    /// </summary>
    [Required]
    public string ModifiedBy { get; set; } = string.Empty;
}
