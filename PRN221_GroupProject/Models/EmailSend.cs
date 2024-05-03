using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class EmailSend
{
    public string EmailSendId { get; set; } = null!;

    public int Id { get; set; }

    public string TemplateId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Sendate { get; set; }
}
