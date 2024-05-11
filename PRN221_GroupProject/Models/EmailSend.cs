﻿using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class EmailSend
{
    public int Id { get; set; }

    public string EmailSendId { get; set; } = null!;

    public string TemplateId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Sendate { get; set; }
}
