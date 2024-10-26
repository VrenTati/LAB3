using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Lecturer
{
    public int LecturerId { get; set; }

    public string LecturerName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Department { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
