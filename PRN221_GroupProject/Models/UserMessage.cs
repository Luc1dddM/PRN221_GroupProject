using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class UserMessage
{
    public int Id { get; set; }

    public string ReceiverId { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public bool Status { get; set; }

    public virtual Message? Message { get; set; } = null!;

    public virtual ApplicationUser? Receiver { get; set; } = null!;
}
