using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartDetail
{
    public string CartDetail1 { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Count { get; set; }
}
