using System;
using System.Collections.Generic;

namespace LAB3.Models;

public partial class Upload
{
    public int UploadId { get; set; }

    public int ResourceId { get; set; }

    public string FilePath { get; set; } = null!;

    public int? UploadedBy { get; set; }

    public DateTime? UploadDate { get; set; }

    public virtual Resource Resource { get; set; } = null!;

    public virtual User? UploadedByNavigation { get; set; }
}
