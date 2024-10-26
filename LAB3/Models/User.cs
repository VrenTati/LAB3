using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string? Role { get; set; }

    public virtual ICollection<AccessControl> AccessControls { get; set; } = new List<AccessControl>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Upload> Uploads { get; set; } = new List<Upload>();
}
