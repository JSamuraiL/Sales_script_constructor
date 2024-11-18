using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class ScriptCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Script> Scripts { get; set; } = new List<Script>();
}
