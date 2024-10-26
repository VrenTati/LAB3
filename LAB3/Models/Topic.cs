using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Topic
{
    public int TopicId { get; set; }

    public int CourseId { get; set; }

    public string TopicName { get; set; } = null!;

    public string? Content { get; set; }

    public virtual Course Course { get; set; } = null!;
}
