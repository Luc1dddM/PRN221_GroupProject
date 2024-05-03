using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class EmailTemplate
{
    public string EmailTemplateId { get; set; } = null!;

    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedDate { get; set; }

    public string? UpdatedBy { get; set; }
}
