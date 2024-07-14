using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Message
{
    public int Id { get; set; }

    public string SenderId { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public string MessageId { get; set; } = null!;

    public string MessageContent { get; set; } = null!;

    public DateTime SendDate { get; set; }

    public virtual Group GroupNameNavigation { get; set; } = null!;

    public virtual ApplicationUser Sender { get; set; } = null!;

    public virtual ICollection<UserMessage> UserMessages { get; set; } = new List<UserMessage>();
}
