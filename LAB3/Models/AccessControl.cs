using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class AccessControl
{
    public int AccessId { get; set; }

    public int ResourceId { get; set; }

    public int? UserId { get; set; }

    public string? Role { get; set; }

    public DateTime? AccessStart { get; set; }

    public DateTime? AccessEnd { get; set; }

    public bool? Restricted { get; set; }

    public virtual Resource Resource { get; set; } = null!;

    public virtual User? User { get; set; }
}
