using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class Group
{
    public int Id { get; set; }

    public string GroupId { get; set; } = null!;

    public string GroupName { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
