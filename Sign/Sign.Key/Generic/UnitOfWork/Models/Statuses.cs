using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Models;

public class Statuses: Dates
{
    /// <summary>
    /// Status Property
    /// </summary>
    public string Status { get; set; } = Enumeration.Status.Active.ToString();
}
