using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Glossary
{
    public int GlossaryId { get; set; }

    public int CourseId { get; set; }

    public string Term { get; set; } = null!;

    public string Definition { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
