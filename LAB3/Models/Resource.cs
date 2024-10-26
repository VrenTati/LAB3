using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Resource
{
    public int ResourceId { get; set; }

    public int CourseId { get; set; }

    public string ResourceType { get; set; } = null!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Alignment { get; set; }

    public string? Tooltip { get; set; }

    public List<string>? Tags { get; set; }

    public virtual ICollection<AccessControl> AccessControls { get; set; } = new List<AccessControl>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<ResourceProperty> ResourceProperties { get; set; } = new List<ResourceProperty>();

    public virtual ICollection<Upload> Uploads { get; set; } = new List<Upload>();
}
