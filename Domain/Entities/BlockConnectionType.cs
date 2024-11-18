using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class BlockConnectionType
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public virtual ICollection<BlockConnection> BlockConnections { get; set; } = new List<BlockConnection>();
}
