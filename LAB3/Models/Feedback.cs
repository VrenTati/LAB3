using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int CourseId { get; set; }

    public string FeedbackText { get; set; } = null!;

    public int? ProvidedBy { get; set; }

    public DateTime? DateProvided { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User? ProvidedByNavigation { get; set; }
}
