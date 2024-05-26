using System;
using System.Collections.Generic;

namespace PRN221_GroupProject.Models;

public partial class CartDetail
{
    public int Id { get; set; }

    public string CartDetail1 { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int Count { get; set; }

    public string CartId { get; set; } = null!;

    public virtual CartHeader Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
