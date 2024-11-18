using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class Manager
{
    public Guid Id { get; set; }

    public string Mail { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public virtual ICollection<Script> Scripts { get; set; } = new List<Script>();

    public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
