using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Subunit
{
    public int SubunitId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Subtitle { get; set; }

    public string? TextContent { get; set; }

    public virtual Course Course { get; set; } = null!;
}
