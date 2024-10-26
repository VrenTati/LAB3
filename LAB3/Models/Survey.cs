using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Survey
{
    public int SurveyId { get; set; }

    public int CourseId { get; set; }

    public string SurveyTitle { get; set; } = null!;

    public string? Description { get; set; }

    public virtual Course Course { get; set; } = null!;
}
