using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class Block
{
    public int Id { get; set; }

    public int ScriptId { get; set; }

    public string? Content { get; set; }

    public virtual ICollection<BlockConnection> BlockConnectionNextBlocks { get; set; } = new List<BlockConnection>();

    public virtual ICollection<BlockConnection> BlockConnectionPreviousBlocks { get; set; } = new List<BlockConnection>();

    public virtual Script Script { get; set; } = null!;
}
