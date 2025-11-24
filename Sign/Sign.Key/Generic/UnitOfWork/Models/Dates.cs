using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Models;

public class Dates
{
    /// <summary>
    /// Created On Property
    /// </summary>
    [Required]
    public long CreatedOn { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

    /// <summary>
    /// Modified On Property
    /// </summary>
    [Required]
    public long ModifiedOn { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
