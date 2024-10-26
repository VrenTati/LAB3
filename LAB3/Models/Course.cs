using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int LecturerId { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Glossary> Glossaries { get; set; } = new List<Glossary>();

    public virtual Lecturer Lecturer { get; set; } = null!;

    public virtual ICollection<Resource> Resources { get; set; } = new List<Resource>();

    public virtual ICollection<Subunit> Subunits { get; set; } = new List<Subunit>();

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
}
