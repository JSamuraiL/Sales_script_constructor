using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class BlockConnection
{
    public int Id { get; set; }

    public int BlockConnectionTypeId { get; set; }

    public int PreviousBlockId { get; set; }

    public int NextBlockId { get; set; }

    public virtual BlockConnectionType BlockConnectionType { get; set; } = null!;

    public virtual Block NextBlock { get; set; } = null!;

    public virtual Block PreviousBlock { get; set; } = null!;
}
