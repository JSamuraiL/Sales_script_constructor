using System;
using System.Collections.Generic;

namespace SalesScriptConstructor.Domain.Entities;

public partial class Seller
{
    public Guid Id { get; set; }

    public string Mail { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public Guid? ManagerId { get; set; }

    public virtual Manager? Manager { get; set; }
}
