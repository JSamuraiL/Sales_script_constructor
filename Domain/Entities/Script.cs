using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class Script
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CreatorId { get; set; }

    public virtual ICollection<Block> Blocks { get; set; } = new List<Block>();

    public virtual Manager Creator { get; set; } = null!;
}
