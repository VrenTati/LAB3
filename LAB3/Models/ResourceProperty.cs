using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class ResourceProperty
{
    public int PropertyId { get; set; }

    public int ResourceId { get; set; }

    public bool? Visibility { get; set; }

    public string? SettingType { get; set; }

    public string? Value { get; set; }

    public virtual Resource Resource { get; set; } = null!;
}
